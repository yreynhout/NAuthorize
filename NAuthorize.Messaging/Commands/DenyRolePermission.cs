using System;

namespace NAuthorize.Messaging.Commands {
  public class DenyRolePermission {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public DenyRolePermission(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}