using System;

namespace NAuthorize.Messaging.Events {
  public class RemovedPermissionFromRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RemovedPermissionFromRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}