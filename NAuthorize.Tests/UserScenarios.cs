using System;
using AggregateSource.Testing;
using NAuthorize.Messaging.Commands;
using NAuthorize.Messaging.Events;
using NUnit.Framework;

namespace NAuthorize.Tests {
  [TestFixture]
  public class UserScenarios {
    public static readonly Guid UserId = Guid.NewGuid();
    public static readonly Guid RoleId = Guid.NewGuid();

    [Test]
    public void when_creating_a_new_user() {
      Scenario.
        Given(UserId,
              new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        Given(RoleId,
              new AddedRole(RoleId, "Administrators")).
        When(new GrantRoleToUser(UserId, RoleId)).
        Then(UserId,
             new RoleGrantedToUser(UserId, RoleId)).
        Assert();
    }
  }
}
