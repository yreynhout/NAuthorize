using System;

namespace NAuthorize.Messaging.Events {
  public class RoleGrantedToUser {
    public readonly Guid UserId;
    public readonly Guid RoleId;

    public RoleGrantedToUser(Guid userId, Guid roleId) {
      UserId = userId;
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Granted role with id {0} to user with id {1}", RoleId, UserId);
    }
  }
}