using System;

namespace NAuthorize.Messaging.Events {
  public class AddedUserGroup {
    public readonly Guid UserGroupId;
    public readonly string Name;

    public AddedUserGroup(Guid userGroupId, string name) {
      UserGroupId = userGroupId;
      Name = name;
    }
  }
}