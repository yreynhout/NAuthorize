using System;

namespace NAuthorize.Messaging.Events {
  public class AddedPermissionToRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public AddedPermissionToRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}