using System;

namespace NAuthorize.Messaging.Events {
  public class RemovedUserFromUserGroup {
    public readonly Guid UserId;
    public readonly Guid UserGroupId;

    public RemovedUserFromUserGroup(Guid userId, Guid userGroupId) {
      UserId = userId;
      UserGroupId = userGroupId;
    }
  }
}