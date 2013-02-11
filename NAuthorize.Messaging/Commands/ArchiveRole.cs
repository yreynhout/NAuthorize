using System;

namespace NAuthorize.Messaging.Commands {
  public class ArchiveRole {
    public readonly Guid RoleId;

    public ArchiveRole(Guid roleId) {
      RoleId = roleId;
    }

    public override string ToString() {
      return string.Format("Archiving role with id {0}", RoleId);
    }
  }
}