using System;
using System.Collections.Generic;

namespace NAuthorize.Application {
  public class ConventionalAdministrativePermissionResolver : IPermissionResolver {
    public IEnumerable<PermissionId> ResolvePermission(object message) {
      if(!ReferenceEquals(message, null))
        yield return new PermissionId(new Uri(string.Format("urn:nauthorize-v1:{0}", message.GetType().Name.ToLowerInvariant())));
    }
  }
}