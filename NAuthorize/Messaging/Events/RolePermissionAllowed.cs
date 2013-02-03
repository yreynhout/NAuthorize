using System;

namespace NAuthorize.Messaging.Events {
  public class RolePermissionAllowed {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RolePermissionAllowed(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}