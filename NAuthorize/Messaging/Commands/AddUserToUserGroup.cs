using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUserToUserGroup {
    public readonly Guid UserId;
    public readonly Guid UserGroupId;

    public AddUserToUserGroup(Guid userId, Guid userGroupId) {
      UserId = userId;
      UserGroupId = userGroupId;
    }
  }
}