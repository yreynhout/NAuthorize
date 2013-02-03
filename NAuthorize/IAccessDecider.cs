using System.Collections.Generic;

namespace NAuthorize {
  public interface IAccessDecider {
    bool AreAllAllowed(IEnumerable<PermissionId> permissionIds);
    bool IsAllowed(PermissionId permissionId);
  }
}