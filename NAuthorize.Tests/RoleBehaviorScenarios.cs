using System;
using AggregateSource.Testing;
using NAuthorize.Messaging.Commands;
using NAuthorize.Messaging.Events;
using NAuthorize.Tests.Infrastructure;
using NUnit.Framework;

namespace NAuthorize.Tests {
  [TestFixture]
  public class RoleBehaviorScenarios {
    static readonly Guid RoleId = Guid.NewGuid();
    const string RoleName = "Administrator";
    static readonly Uri PermissionId = new Uri("urn:permission:123");
    static readonly Uri UnknownPermissionId = new Uri("urn:permission:456");

    [Test]
    public void when_adding_a_new_role() {
      Scenario.
        When(new AddRole(RoleId, RoleName)).
        Then(RoleId, 
          new AddedRole(RoleId, RoleName)).
        Assert();
    }

    [Test]
    public void when_allowing_a_permission() {
      Scenario.
        Given(RoleId, 
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId)).
        When(new AllowRolePermission(RoleId, PermissionId)).
        Then(RoleId,
          new RolePermissionAllowed(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_allowing_an_allowed_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId),
          new RolePermissionAllowed(RoleId, PermissionId)).
        When(new AllowRolePermission(RoleId, PermissionId)).
        AssertNothingHappened();
    }

    [Test]
    public void when_denying_an_allowed_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId),
          new RolePermissionAllowed(RoleId, PermissionId)).
        When(new DenyRolePermission(RoleId, PermissionId)).
        Then(RoleId,
          new RolePermissionDenied(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_allowing_an_unknown_permission() {
      Scenario.
        Given(RoleId, 
          new AddedRole(RoleId, RoleName)).
        When(new AllowRolePermission(RoleId, UnknownPermissionId)).
        AssertThrows(new Exception("Yo bro, the permission is not known to me."));
    }

    [Test]
    public void when_denying_a_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId)).
        When(new AllowRolePermission(RoleId, PermissionId)).
        Then(RoleId,
          new RolePermissionAllowed(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_denying_a_denied_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId),
          new RolePermissionDenied(RoleId, PermissionId)).
        When(new DenyRolePermission(RoleId, PermissionId)).
        AssertNothingHappened();
    }

    [Test]
    public void when_allowing_a_denied_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId),
          new RolePermissionDenied(RoleId, PermissionId)).
        When(new AllowRolePermission(RoleId, PermissionId)).
        Then(RoleId,
          new RolePermissionAllowed(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_denying_an_unknown_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName)).
        When(new DenyRolePermission(RoleId, UnknownPermissionId)).
        AssertThrows(new Exception("Yo bro, the permission is not known to me."));
    }

    [Test]
    public void when_adding_a_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName)).
        When(new AddPermissionToRole(RoleId, PermissionId)).
        Then(RoleId,
          new AddedPermissionToRole(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_adding_an_added_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId)).
        When(new AddPermissionToRole(RoleId, PermissionId)).
        AssertThrows(new Exception("Yo bro, the permission is already known to me."));
    }

    [Test]
    public void when_removing_a_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId)).
        When(new RemovePermissionFromRole(RoleId, PermissionId)).
        Then(RoleId,
          new RemovedPermissionFromRole(RoleId, PermissionId)).
        Assert();
    }

    [Test]
    public void when_removing_an_unknown_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName)).
        When(new RemovePermissionFromRole(RoleId, UnknownPermissionId)).
        AssertThrows(new Exception("Yo bro, the permission is not known to me."));
    }

    [Test]
    public void when_removing_a_removed_permission() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new AddedPermissionToRole(RoleId, PermissionId),
          new RemovedPermissionFromRole(RoleId, PermissionId)).
        When(new RemovePermissionFromRole(RoleId, PermissionId)).
        AssertThrows(new Exception("Yo bro, the permission is not known to me."));
    }

    [Test]
    public void when_archiving_a_role() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName)).
        When(new ArchiveRole(RoleId)).
        Then(RoleId,
          new ArchivedRole(RoleId)).
        Assert();
    }

    [Test]
    public void when_archiving_an_archived_role() {
      Scenario.
        Given(RoleId,
          new AddedRole(RoleId, RoleName),
          new ArchivedRole(RoleId)).
        When(new ArchiveRole(RoleId)).
        AssertNothingHappened();
    }
  }
}
