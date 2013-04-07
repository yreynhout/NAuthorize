using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AggregateSource;
using Autofac;
using NAuthorize.Application;
using NAuthorize.Messaging.Commands;
using StreamSource;

namespace NAuthorize.Tests.Infrastructure {
  public static class CompositionRoot {
    public static readonly ILifetimeScope Instance = Compose();

    static ILifetimeScope Compose() {
      var builder = new ContainerBuilder();
      builder.
        RegisterType<UnitOfWork>().
        AsSelf().
        InstancePerLifetimeScope();
      builder.
        RegisterGeneric(typeof (Repository<>)).
        AsImplementedInterfaces().InstancePerLifetimeScope();
      builder.
        RegisterAssemblyTypes(typeof (UserApplicationService).Assembly).
        AsClosedTypesOf(typeof (IHandle<>)).
        InstancePerDependency();
      builder.
        RegisterType<MemoryEventStoreReader>().
        AsImplementedInterfaces().
        InstancePerLifetimeScope();
      builder.
        RegisterInstance(User.Factory).
        SingleInstance();
      builder.
        RegisterInstance(Role.Factory).
        SingleInstance();
      builder.
        RegisterType<Dictionary<string, List<object>>>().
        InstancePerLifetimeScope();

      foreach (var commandType in AllCommandTypes) {
        builder.
          RegisterType(typeof (HandlerAdapter<>).MakeGenericType(commandType)).
          Keyed<IHandle<object>>(commandType).
          InstancePerLifetimeScope();
      }

      return builder.Build();
    }

    static IEnumerable<Type> AllCommandTypes {
      get {
        return from types in Assembly.GetAssembly(typeof(AddRole)).GetTypes()
               where types.Namespace != null && types.Namespace.StartsWith("NAuthorize.Messaging.Commands")
               select types;
      }
    }
  }
}