using System;

namespace NAuthorize.Messaging.Events {
  public class RoleGrantedToUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RoleGrantedToUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }
  }
}