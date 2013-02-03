using System;

namespace NAuthorize.Messaging.Events {
  public class DisabledUserGroup {
    public readonly Guid UserGroupId;

    public DisabledUserGroup(Guid userGroupId) {
      UserGroupId = userGroupId;
    }
  }
}