using System;
using AggregateSource.Testing;
using NAuthorize.Messaging.Commands;
using NAuthorize.Messaging.Events;
using NAuthorize.Tests.Infrastructure;
using NUnit.Framework;

namespace NAuthorize.Tests {
  [TestFixture]
  public class UserBehaviorScenarios {
    public static readonly Guid UserId = Guid.NewGuid();
    public static readonly Guid RoleId = Guid.NewGuid();

    [Test]
    public void when_creating_a_new_user() {
      Scenario.
        When(new AddUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        Then(UserId,
             new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        Assert();
    }

    [Test]
    public void when_granting_a_role_to_a_user() {
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

    [Test]
    public void when_granting_a_role_to_a_disabled_user() {
      Scenario.
        Given(UserId,
              new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3")),
              new DisabledUser(UserId)).
        Given(RoleId,
              new AddedRole(RoleId, "Administrators")).
        When(new GrantRoleToUser(UserId, RoleId)).
        AssertThrows(new Exception("Yo bro, you can't mutate this thing. It's been disabled!"));
    }

    [Test]
    public void when_revoking_a_role_that_was_never_granted_to_a_user() {
      Scenario.
        Given(UserId,
              new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        Given(RoleId,
              new AddedRole(RoleId, "Administrators")).
        When(new RevokeRoleFromUser(UserId, RoleId)).
        AssertThrows(new Exception("Yo bro, this role was never granted in the first place!"));
    }

    [Test]
    public void when_revoking_a_role_from_a_user() {
      Scenario.
        Given(UserId,
              new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        Given(RoleId,
              new AddedRole(RoleId, "Administrators")).
        Given(UserId,
              new RoleGrantedToUser(UserId, RoleId)).
        When(new RevokeRoleFromUser(UserId, RoleId)).
        Then(UserId,
              new RoleRevokedFromUser(UserId, RoleId)).
        Assert();
    }

    [Test]
    public void when_revoking_a_role_from_a_disabled_user() {
      Scenario.
        Given(UserId,
              new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3")),
              new DisabledUser(UserId)).
        Given(RoleId,
              new AddedRole(RoleId, "Administrators")).
        Given(UserId,
              new RoleGrantedToUser(UserId, RoleId)).
        When(new RevokeRoleFromUser(UserId, RoleId)).
        AssertThrows(new Exception("Yo bro, you can't mutate this thing. It's been disabled!"));
    }

    [Test]
    public void when_disabling_a_user() {
      Scenario.
        Given(UserId, new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3"))).
        When(new DisableUser(UserId)).
        Then(UserId,
             new DisabledUser(UserId)).
        Assert();
    }

    [Test]
    public void when_disabling_a_disabled_user() {
      Scenario.
        Given(UserId, 
          new AddedUser(UserId, new Uri("urn:windows:sid:S-1-2-3")),
          new DisabledUser(UserId)).
        When(new DisableUser(UserId)).
        AssertNothingHappened();
    }
  }
}
