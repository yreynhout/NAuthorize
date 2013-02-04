using System;
using System.Collections.Generic;
using AggregateSource;
using NAuthorize.Messaging.Events;

namespace NAuthorize {
  public class User : AggregateRootEntity {
    public static readonly Func<User> Factory = () => new User();

    User() {
      Register<AddedUser>(When);
      Register<DisabledUser>(When);
      Register<RoleGrantedToUser>(When);
      Register<RoleRevokedFromUser>(When);
      Register<AddedUserToUserGroup>(When);
      Register<RemovedUserFromUserGroup>(When);
    }

    public UserId Id { get; private set; }

    // Behavior

    public User(UserId userId, UserIdentifier identifier) : this() {
      Apply(
        new AddedUser(userId, identifier));
    }

    public void GrantRole(Role role) {
      ThrowIfDisabled();
      Apply(
        new RoleGrantedToUser(Id, role.Id));
    }

    public void GrantUserGroup(UserGroup userGroup) {
      ThrowIfDisabled();
      Apply(
        new AddedUserToUserGroup(Id, userGroup.Id));
    }

    public void RevokeRole(Role role) {
      ThrowIfDisabled();
      Apply(
        new RoleRevokedFromUser(Id, role.Id));
    }

    public void RevokeUserGroup(UserGroup userGroup) {
      ThrowIfDisabled();
      Apply(
        new RemovedUserFromUserGroup(Id, userGroup.Id));
    }

    public void Disable() {
      if (!_disabled)
        Apply(
          new DisabledUser(Id));
    }

    public void CombineDecisions(IAccessDecisionCombinator combinator, IRepository<Role> roleRepository,
                                 IRepository<UserGroup> userGroupRepository) {
      if (_disabled) return;

      foreach (var roleId in _roles) {
        roleRepository.Get(roleId).CombineDecisions(combinator);
      }

      foreach (var userGroupId in _userGroups) {
        userGroupRepository.Get(userGroupId).CombineDecisions(combinator, roleRepository);
      }
    }

    void ThrowIfDisabled() {
      if (_disabled)
        throw new Exception("Yo bro, you can't mutate this thing. It's been disabled!");
    }

    // State

    HashSet<RoleId> _roles;
    HashSet<UserGroupId> _userGroups;
    bool _disabled;

    void When(AddedUser @event) {
      Id = new UserId(@event.UserId);
      _roles = new HashSet<RoleId>();
      _userGroups = new HashSet<UserGroupId>();
      _disabled = false;
    }

    void When(DisabledUser @event) {
      _disabled = true;
      _roles.Clear();
      _userGroups.Clear();
    }

    void When(RoleGrantedToUser @event) {
      _roles.Add(new RoleId(@event.RoleId));
    }

    void When(RoleRevokedFromUser @event) {
      _roles.Remove(new RoleId(@event.RoleId));
    }

    void When(AddedUserToUserGroup @event) {
      _userGroups.Add(new UserGroupId(@event.UserGroupId));
    }

    void When(RemovedUserFromUserGroup @event) {
      _userGroups.Remove(new UserGroupId(@event.UserGroupId));
    }
  }
}