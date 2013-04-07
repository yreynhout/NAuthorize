using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Xml;
using AggregateSource;
using AggregateSource.GEventStore;
using EventStore.ClientAPI;
using NAuthorize.Application;
using NDesk.Options;
using NLog;
using ProtoBuf;

namespace NAuthorize.Importer {
  class Program {
    static readonly Logger Log = LogManager.GetCurrentClassLogger();

    static void Main(string[] args) {
      string usersFilePath = null;

      var options = new OptionSet {
        {"i|input:", "Path to file that contains names and SIDs of users to be imported.", value => usersFilePath = value}
      };

      try {
        options.Parse(args);

        if (usersFilePath != null) {
          var watch = Stopwatch.StartNew();
          var identity = WindowsIdentity.GetCurrent();
          Log.Info("Importing {0} at {1} as {2}.",
                    usersFilePath,
                    XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Local),
                    identity != null ? identity.Name : "<unknown>");

          using (var connection = EventStoreConnection.Create()) {
            connection.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));

            using (var file = File.OpenText(usersFilePath)) {
              string row;
              while ((row = file.ReadLine()) != null) {
                Record record;
                if (Record.TryParse(row, out record)) {
                  var unitOfWork = new UnitOfWork();

                  IRepository<User> userRepository =
                    new Repository<User>(User.Factory, unitOfWork, connection);
                  IRepository<Role> roleRepository =
                    new Repository<Role>(Role.Factory, unitOfWork, connection);
                  var service = new UserApplicationService(userRepository, roleRepository);
                  var command = record.ToCommand();
                  Console.WriteLine("Start handling command: {0} - {1}", command.GetType().Name, command.Identifier);
                  service.Handle(command);
                  if (unitOfWork.HasChanges()) {
                    const int sliceEventCount = 500;
                    var aggregate = unitOfWork.GetChanges().Single();
                    var eventIndex = 0;
                    var slices = from eventData in
                                   aggregate.Root.
                                             GetChanges().
                                             Select(change => new EventData(
                                                                Guid.NewGuid(),
                                                                change.GetType().AssemblyQualifiedName,
                                                                false,
                                                                SerializeEvent(change),
                                                                new byte[0]))
                                 group eventData by eventIndex++%sliceEventCount
                                 into slice
                                 select slice.AsEnumerable();
                    using (var transaction = connection.StartTransaction(
                      aggregate.Identifier, 
                      aggregate.ExpectedVersion)) {
                      foreach (var slice in slices) {
                        transaction.Write(slice);
                      }
                      transaction.Commit();
                      Console.WriteLine("Committed events for command");
                    }
                  }

                } else {
                  Log.Error("Could not import the following row. Please make sure it's well formed: {0}", row);
                }
              }
            }
          }

          Log.Info("Completed import at {0} (took {1}ms).",
            XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Local),
            watch.ElapsedMilliseconds);
        } else {
          options.WriteOptionDescriptions(Console.Out);
        }
      } catch (OptionException exception) {
        Console.WriteLine(exception.Message);
      }
    }

    static byte[] SerializeEvent(object @event) {
      using (var stream = new MemoryStream()) {
        Serializer.NonGeneric.Serialize(stream, @event);
        return stream.ToArray();
      }
    }
  }
}
