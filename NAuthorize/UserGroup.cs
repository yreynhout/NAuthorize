using System;
using System.Collections.Generic;
using AggregateSource;
using NAuthorize.Messaging.Events;

namespace NAuthorize {
  public class UserGroup : AggregateRootEntity {
    public static readonly Func<UserGroup> Factory = () => new UserGroup();

    UserGroup() {
      Register<AddedUserGroup>(When);
      Register<DisabledUserGroup>(When);
      Register<GrantedRoleToUserGroup>(When);
      Register<RevokedRoleFromUserGroup>(When);
    }

    public UserGroupId Id { get; private set; }

    // Behavior

    public UserGroup(UserGroupId userGroupId, Name name) : this() {
      Apply(
        new AddedUserGroup(userGroupId, name));
    }

    public void Archive() {
      if (!_archived)
        Apply(
          new DisabledUserGroup(Id));
    }

    public void AssignRole(Role role) {
      ThrowIfArchived();
      Apply(
        new GrantedRoleToUserGroup(Id, role.Id));
    }

    public void RevokeRole(Role role) {
      ThrowIfArchived();
      Apply(
        new RevokedRoleFromUserGroup(Id, role.Id));
    }

    public void CombineDecisions(IAccessDecisionCombinator combinator, IRepository<Role> roleRepository) {
      if (_archived) return;
      foreach (var roleId in _roles) {
        roleRepository.
          Get(roleId).
          CombineDecisions(combinator);
      }
    }

    void ThrowIfArchived() {
      if (_archived)
        throw new Exception("Yo bro, you can't mutate this thing. It's been archived!");
    }

    // State

    HashSet<RoleId> _roles;
    bool _archived;

    void When(AddedUserGroup @event) {
      Id = new UserGroupId(@event.UserGroupId);
      _roles = new HashSet<RoleId>();
      _archived = false;
    }

    void When(DisabledUserGroup @event) {
      _archived = true;
      _roles.Clear();
    }

    void When(GrantedRoleToUserGroup @event) {
      _roles.Add(new RoleId(@event.RoleId));
    }

    void When(RevokedRoleFromUserGroup @event) {
      _roles.Remove(new RoleId(@event.RoleId));
    }
  }
}