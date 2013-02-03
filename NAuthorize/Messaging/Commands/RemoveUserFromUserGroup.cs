using System;

namespace NAuthorize.Messaging.Commands {
  public class RemoveUserFromUserGroup {
    public readonly Guid UserId;
    public readonly Guid UserGroupId;

    public RemoveUserFromUserGroup(Guid userId, Guid userGroupId) {
      UserId = userId;
      UserGroupId = userGroupId;
    }
  }
}