using System;

namespace NAuthorize.Messaging.Events {
  public class RolePermissionAllowed {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RolePermissionAllowed(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Allowed permission {0} in role with id {1}", PermissionId, RoleId);
    }
  }
}