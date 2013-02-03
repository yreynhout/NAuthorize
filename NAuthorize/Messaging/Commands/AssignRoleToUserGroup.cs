using System;

namespace NAuthorize.Messaging.Commands {
  public class AssignRoleToUserGroup {
    public readonly Guid UserGroupId;
    public readonly Guid RoleId;

    public AssignRoleToUserGroup(Guid userGroupId, Guid roleId) {
      UserGroupId = userGroupId;
      RoleId = roleId;
    }
  }
}