using System;

namespace NAuthorize.Messaging.Events {
  public class AddedUserToUserGroup {
    public readonly Guid UserId;
    public readonly Guid UserGroupId;

    public AddedUserToUserGroup(Guid userId, Guid userGroupId) {
      UserId = userId;
      UserGroupId = userGroupId;
    }
  }
}