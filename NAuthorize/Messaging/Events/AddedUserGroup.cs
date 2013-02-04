using System;

namespace NAuthorize.Messaging.Events {
  public class AddedUserGroup {
    public readonly Guid UserGroupId;
    public readonly Uri Identifier;

    public AddedUserGroup(Guid userGroupId, Uri identifier) {
      UserGroupId = userGroupId;
      Identifier = identifier;
    }
  }
}