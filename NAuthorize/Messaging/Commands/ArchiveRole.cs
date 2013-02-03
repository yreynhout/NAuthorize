using System;

namespace NAuthorize.Messaging.Commands {
  public class ArchiveRole {
    public readonly Guid RoleId;

    public ArchiveRole(Guid roleId) {
      RoleId = roleId;
    }
  }
}