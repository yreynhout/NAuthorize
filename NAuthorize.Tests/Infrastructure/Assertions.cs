using System;
using System.Collections.Generic;
using System.Linq;
using AggregateSource;
using AggregateSource.Testing;
using Autofac;
using NAuthorize.Application;
using NUnit.Framework;

namespace NAuthorize.Tests.Infrastructure {
  public static class Assertions {
    public static void Assert(this IThenStateBuilder builder) {
      using (var scope = CompositionRoot.Instance.BeginLifetimeScope()) {
        var specification = builder.Build();
        
        var storage = scope.Resolve<Dictionary<Guid, List<object>>>();
        foreach (var item in specification.
          Givens.
          GroupBy(given => given.Item1).
          ToDictionary(group => group.Key, group => group.Select(item => item.Item2).ToList())) {
          storage.Add(item.Key, item.Value);
        }

        scope.ResolveKeyed<IHandle<object>>(specification.When.GetType()).Handle(specification.When);

        var unitOfWork = scope.Resolve<UnitOfWork>();
        var actualEvents = unitOfWork.GetChanges().Single().Root.GetChanges().ToArray();
        var expectedEvents = specification.Thens.Select(item => item.Item2).ToArray();

        var comparer = new CompareObjects();
        if (!comparer.Compare(actualEvents, expectedEvents)) {
          //The actual events do not match the expected events
          NUnit.Framework.Assert.Fail(comparer.DifferencesString);
        }
      }
    }

    public static void AssertThrows(this IWhenStateBuilder builder, Exception expectedException) {
      using (var scope = CompositionRoot.Instance.BeginLifetimeScope()) {
        var specification = builder.Throws(expectedException).Build();

        var storage = scope.Resolve<Dictionary<Guid, List<object>>>();
        foreach (var item in specification.
          Givens.
          GroupBy(given => given.Item1).
          ToDictionary(group => group.Key, group => group.Select(item => item.Item2).ToList())) {
          storage.Add(item.Key, item.Value);
        }

        try {
          scope.ResolveKeyed<IHandle<object>>(specification.When.GetType()).Handle(specification.When);
          NUnit.Framework.Assert.Fail(
            "Expected the following exception to be thrown:\n\tType:{0}\n\tMessage:{1}.",
            specification.Throws.GetType().Name,
            specification.Throws.Message);
        }
        catch (AssertionException) {
          throw;
        }
        catch (Exception actualException) {
          NUnit.Framework.Assert.That(
            actualException.GetType(), 
            Is.EqualTo(specification.Throws.GetType()),
            "Expected an exception of type {0} but was of type {1}.", 
            specification.Throws.GetType().Name, 
            actualException.GetType().Name);

          NUnit.Framework.Assert.That(
            actualException.Message, 
            Is.EqualTo(specification.Throws.Message),
            "Expected an exception with a specific message:\n\tExpected Message:{0}\n\tActual Message:{1}", 
            specification.Throws.Message, actualException.Message);
        }
      }
    }

    public static void AssertNothingHappened(this IWhenStateBuilder builder) {
      using (var scope = CompositionRoot.Instance.BeginLifetimeScope()) {
        var specification = builder.Build();

        var storage = scope.Resolve<Dictionary<Guid, List<object>>>();
        foreach (var item in specification.
          Givens.
          GroupBy(given => given.Item1).
          ToDictionary(group => group.Key, group => group.Select(item => item.Item2).ToList())) {
          storage.Add(item.Key, item.Value);
        }

        scope.ResolveKeyed<IHandle<object>>(specification.When.GetType()).Handle(specification.When);

        var unitOfWork = scope.Resolve<UnitOfWork>();
        NUnit.Framework.Assert.That(unitOfWork.GetChanges(), Is.Empty, 
          "Expected no events but found the following events:\n\t{0}",
          string.Join(",", unitOfWork.GetChanges().SelectMany(aggregate => aggregate.Root.GetChanges()).Select(@event => @event.GetType().Name)));
      }
    }
  }
}