using System;

namespace NAuthorize.Messaging.Events {
  public class RemovedPermissionFromRole {
    public readonly Guid RoleId;
    public readonly Uri PermissionId;

    public RemovedPermissionFromRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }

    public override string ToString() {
      return string.Format("Removed permission {0} from role with id {1}", PermissionId, RoleId);
    }
  }
}