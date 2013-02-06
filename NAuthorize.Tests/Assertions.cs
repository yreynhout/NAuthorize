using System;
using System.Collections.Generic;
using System.Linq;
using AggregateSource;
using AggregateSource.Testing;
using KellermanSoftware.CompareNetObjects;
using NAuthorize.Application;
using NAuthorize.Messaging.Commands;

namespace NAuthorize.Tests {
  public static class Assertions {
    public static void Assert(this IThenStateBuilder builder) {
      var specification = builder.Build();

      var unitOfWork = new UnitOfWork();
      var storage = specification.
        Givens.
        GroupBy(given => given.Item1).
        ToDictionary(group => group.Key, group => group.Select(item => item.Item2).ToList());
      Func<Guid, Tuple<int, IEnumerable<object>>> eventStreamReader = new MemoryEventStoreReader(storage).Read;
      IRepository<User> userRepository = new Repository<User>(User.Factory,unitOfWork, eventStreamReader);
      IRepository<Role> roleRepository=new Repository<Role>(Role.Factory, unitOfWork, eventStreamReader);
      var service = new UserApplicationService(userRepository, roleRepository);

      service.Handle((GrantRoleToUser) specification.When);

      var actualEvents = unitOfWork.GetChanges().Single().Root.GetChanges().ToArray();
      var expectedEvents = specification.Thens.Select(item => item.Item2).ToArray();

      var comparer = new CompareObjects();
      if (!comparer.Compare(actualEvents, expectedEvents)) {
        NUnit.Framework.Assert.Fail(comparer.DifferencesString);
      }
    }
  }
}