using System;

namespace NAuthorize.Messaging.Commands {
  public class RevokeRoleFromUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RevokeRoleFromUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Revoking role with id {0} from user with id {1}", RoleId, UserId);
    }
  }
}