using System;

namespace NAuthorize.Messaging.Events {
  public class ArchivedRole {
    public readonly Guid RoleId;

    public ArchivedRole(Guid roleId) {
      RoleId = roleId;
    }
  }
}