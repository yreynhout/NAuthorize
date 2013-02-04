using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUserGroup {
    public readonly Guid UserGroupId;
    public readonly Uri Identifier;

    public AddUserGroup(Guid userGroupId, Uri identifier) {
      UserGroupId = userGroupId;
      Identifier = identifier;
    }
  }
}