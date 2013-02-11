using System;

namespace NAuthorize.Messaging.Commands {
  public class AllowRolePermission {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public AllowRolePermission(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Allow permission {0} in role with id {1}", PermissionId, RoleId);
    }
  }
}