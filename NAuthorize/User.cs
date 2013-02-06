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

    public void RevokeRole(Role role) {
      ThrowIfDisabled();
      Apply(
        new RoleRevokedFromUser(Id, role.Id));
    }

    public void Disable() {
      if (!_disabled)
        Apply(
          new DisabledUser(Id));
    }

    public void CombineDecisions(IAccessDecisionCombinator combinator, IRepository<Role> roleRepository) {
      if (_disabled) return;

      foreach (var roleId in _roles) {
        roleRepository.Get(roleId).CombineDecisions(combinator);
      }
    }

    void ThrowIfDisabled() {
      if (_disabled)
        throw new Exception("Yo bro, you can't mutate this thing. It's been disabled!");
    }

    // State

    HashSet<RoleId> _roles;
    bool _disabled;

    void When(AddedUser @event) {
      Id = new UserId(@event.UserId);
      _roles = new HashSet<RoleId>();
      _disabled = false;
    }

    void When(DisabledUser @event) {
      _disabled = true;
      _roles.Clear();
    }

    void When(RoleGrantedToUser @event) {
      _roles.Add(new RoleId(@event.RoleId));
    }

    void When(RoleRevokedFromUser @event) {
      _roles.Remove(new RoleId(@event.RoleId));
    }
  }
}