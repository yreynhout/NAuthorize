using System.Collections.Generic;

namespace NAuthorize.Application {
  public interface IPermissionResolver {
    IEnumerable<PermissionId> ResolvePermission(object message);
  }
}