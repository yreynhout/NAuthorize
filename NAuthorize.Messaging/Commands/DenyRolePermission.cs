using System;

namespace NAuthorize.Messaging.Commands {
  public class DenyRolePermission {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public DenyRolePermission(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Deny permission {0} in role with id {1}", PermissionId, RoleId);
    }
  }
}