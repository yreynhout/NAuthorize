using System;

namespace NAuthorize.Messaging.Commands {
  public class GrantRoleToUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public GrantRoleToUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Granting role with id {0} to user with id {1}", RoleId, UserId);
    }
  }
}