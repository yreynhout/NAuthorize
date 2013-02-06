using System;
using AggregateSource;
using NAuthorize.Messaging.Commands;

namespace NAuthorize.Application {
  public class UserApplicationService :
    IHandle<AddUser>,
    IHandle<DisableUser>,
    IHandle<GrantRoleToUser>,
    IHandle<RevokeRoleFromUser> {
    readonly IRepository<User> _userRepository;
    readonly IRepository<Role> _roleRepository;

    public UserApplicationService(IRepository<User> userRepository, IRepository<Role> roleRepository) {
      if (userRepository == null) throw new ArgumentNullException("userRepository");
      if (roleRepository == null) throw new ArgumentNullException("roleRepository");
      _userRepository = userRepository;
      _roleRepository = roleRepository;
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

   public void Handle(RevokeRoleFromUser message) {
      var role = _roleRepository.Get(message.RoleId);
      ForUser(message.UserId).RevokeRole(role);
    }

    User ForUser(Guid userId) {
      return _userRepository.Get(new UserId(userId));
    }
  }
}