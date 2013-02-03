using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUserGroup {
    public readonly Guid UserGroupId;
    public readonly string Name;

    public AddUserGroup(Guid userGroupId, string name) {
      UserGroupId = userGroupId;
      Name = name;
    }
  }
}