using System;

namespace NAuthorize.Messaging.Commands {
  public class AddPermissionToRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public AddPermissionToRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Adding permission {0} to role with id {1}", PermissionId, RoleId);
    }
  }
}