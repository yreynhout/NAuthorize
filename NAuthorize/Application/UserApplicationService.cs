using System;
using AggregateSource;
using NAuthorize.Messaging.Commands;

namespace NAuthorize.Application {
  public class UserApplicationService :
    IHandle<AddUser>,
    IHandle<DisableUser>,
    IHandle<AddUserToUserGroup>,
    IHandle<GrantRoleToUser>,
    IHandle<RevokeRoleFromUser>,
    IHandle<RemoveUserFromUserGroup> {
    readonly IRepository<User> _userRepository;
    readonly IRepository<Role> _roleRepository;
    readonly IRepository<UserGroup> _userGroupRepository;

    public UserApplicationService(IRepository<User> userRepository,
                                         IRepository<Role> roleRepository, IRepository<UserGroup> userGroupRepository) {
      if (userRepository == null) throw new ArgumentNullException("userRepository");
      if (roleRepository == null) throw new ArgumentNullException("roleRepository");
      if (userGroupRepository == null) throw new ArgumentNullException("userGroupRepository");
      _userRepository = userRepository;
      _roleRepository = roleRepository;
      _userGroupRepository = userGroupRepository;
    }

    public void Handle(AddUser message) {
      _userRepository.Add(message.UserId,
        new User(
          new UserId(message.UserId),
          new UserIdentifier(message.Identifier)));
    }

    public void Handle(DisableUser message) {
      ForUser(message.UserId).Disable();
    }

    public void Handle(GrantRoleToUser message) {
      var role = _roleRepository.Get(message.RoleId);
      ForUser(message.UserId).GrantRole(role);
    }

    public void Handle(AddUserToUserGroup message) {
      var userGroup = _userGroupRepository.Get(message.UserGroupId);
      ForUser(message.UserId).GrantUserGroup(userGroup);
    }

    public void Handle(RevokeRoleFromUser message) {
      var role = _roleRepository.Get(message.RoleId);
      ForUser(message.UserId).RevokeRole(role);
    }

    public void Handle(RemoveUserFromUserGroup message) {
      var userGroup = _userGroupRepository.Get(message.UserGroupId);
      ForUser(message.UserId).RevokeUserGroup(userGroup);
    }

    User ForUser(Guid userId) {
      return _userRepository.Get(new UserId(userId));
    }
  }
}