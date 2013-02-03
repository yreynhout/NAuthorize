using System;

namespace NAuthorize.Messaging.Events {
  public class RoleRevokedFromUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RoleRevokedFromUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }
  }
}