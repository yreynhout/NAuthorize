using System;

namespace NAuthorize.Messaging.Events {
  public class RolePermissionDenied {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RolePermissionDenied(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Denied permission {0} in role with id {1}", PermissionId, RoleId);
    }
  }
}