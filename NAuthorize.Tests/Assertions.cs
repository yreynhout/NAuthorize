using System;
using System.Collections.Generic;
using System.Linq;
using AggregateSource;
using AggregateSource.Testing;
using Autofac;
using KellermanSoftware.CompareNetObjects;
using NAuthorize.Application;

namespace NAuthorize.Tests {
  public static class Assertions {
    public static void Assert(this IThenStateBuilder builder) {
      using (var scope = CompositionRoot.Instance.BeginLifetimeScope()) {
        var specification = builder.Build();

        var unitOfWork = scope.Resolve<UnitOfWork>();
        var storage = scope.Resolve<Dictionary<Guid, List<Object>>>();
        foreach (var item in specification.
          Givens.
          GroupBy(given => given.Item1).
          ToDictionary(group => group.Key, group => group.Select(item => item.Item2).ToList())) {
          storage.Add(item.Key, item.Value);
        }

        scope.ResolveKeyed<IHandle<object>>(specification.When.GetType()).Handle(specification.When);

        var actualEvents = unitOfWork.GetChanges().Single().Root.GetChanges().ToArray();
        var expectedEvents = specification.Thens.Select(item => item.Item2).ToArray();

        var comparer = new CompareObjects();
        if (!comparer.Compare(actualEvents, expectedEvents)) {
          NUnit.Framework.Assert.Fail(comparer.DifferencesString);
        }
      }
    }
  }
}