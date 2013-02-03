using System;

namespace NAuthorize.Messaging.Commands {
  public class RemovePermissionFromRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RemovePermissionFromRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}