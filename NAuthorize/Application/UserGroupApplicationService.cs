using System;
using AggregateSource;
using NAuthorize.Messaging.Commands;

namespace NAuthorize.Application {
  public class UserGroupApplicationService :
    IHandle<AddUserGroup>,
    IHandle<DisableUserGroup>,
    IHandle<AssignRoleToUserGroup>,
    IHandle<RevokeRoleFromUserGroup> {
    readonly IRepository<UserGroup> _userGroupRepository;
    readonly IRepository<Role> _roleRepository;

    public UserGroupApplicationService(IRepository<UserGroup> userGroupRepository, IRepository<Role> roleRepository) {
      if (userGroupRepository == null) throw new ArgumentNullException("userGroupRepository");
      if (roleRepository == null) throw new ArgumentNullException("roleRepository");
      _userGroupRepository = userGroupRepository;
      _roleRepository = roleRepository;
    }

    public void Handle(AddUserGroup message) {
      _userGroupRepository.Add(message.UserGroupId,
        new UserGroup(new UserGroupId(message.UserGroupId), new UserGroupIdentifier(message.Identifier)));
    }

    public void Handle(DisableUserGroup message) {
      ForUserGroup(message.UserGroupId).Archive();
    }

    public void Handle(AssignRoleToUserGroup message) {
      var role = _roleRepository.Get(message.RoleId);
      ForUserGroup(message.UserGroupId).AssignRole(role);
    }

    public void Handle(RevokeRoleFromUserGroup message) {
      var role = _roleRepository.Get(message.RoleId);
      ForUserGroup(message.UserGroupId).RevokeRole(role);
    }

    UserGroup ForUserGroup(Guid userGroupId) {
      return _userGroupRepository.Get(new UserGroupId(userGroupId));
    }
  }
}