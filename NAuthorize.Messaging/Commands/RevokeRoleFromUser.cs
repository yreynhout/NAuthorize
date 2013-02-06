using System;

namespace NAuthorize.Messaging.Commands {
  public class RevokeRoleFromUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RevokeRoleFromUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }
  }
}