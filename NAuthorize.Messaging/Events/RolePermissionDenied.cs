using System;

namespace NAuthorize.Messaging.Events {
  public class RolePermissionDenied {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RolePermissionDenied(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}