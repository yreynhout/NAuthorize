using System;

namespace NAuthorize.Messaging.Commands {
  public class GrantRoleToUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public GrantRoleToUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }
  }
}