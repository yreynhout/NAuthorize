using System;

namespace NAuthorize.Messaging.Commands {
  public class DisableUserGroup {
    public readonly Guid UserGroupId;

    public DisableUserGroup(Guid userGroupId) {
      UserGroupId = userGroupId;
    }
  }
}