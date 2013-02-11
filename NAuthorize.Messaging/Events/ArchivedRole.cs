using System;

namespace NAuthorize.Messaging.Events {
  public class ArchivedRole {
    public readonly Guid RoleId;

    public ArchivedRole(Guid roleId) {
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Archived role with id {0}", RoleId);
    }
  }
}