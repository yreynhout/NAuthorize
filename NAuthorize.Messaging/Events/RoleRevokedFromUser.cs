using System;

namespace NAuthorize.Messaging.Events {
  public class RoleRevokedFromUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RoleRevokedFromUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Revoked role with id {0} from user with id {1}", RoleId, UserId);
    }
  }
}