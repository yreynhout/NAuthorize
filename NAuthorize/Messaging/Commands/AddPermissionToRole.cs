using System;

namespace NAuthorize.Messaging.Commands {
  public class AddPermissionToRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public AddPermissionToRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}