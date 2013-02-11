using System;

namespace NAuthorize.Messaging.Commands {
  public class RemovePermissionFromRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RemovePermissionFromRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Removing permission {0} from role with id {1}", PermissionId, RoleId);
    }
  }
}