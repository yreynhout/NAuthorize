using System;

namespace NAuthorize.Messaging.Commands {
  public class RevokeRoleFromUserGroup {
    public readonly Guid UserGroupId;
    public readonly Guid RoleId;

    public RevokeRoleFromUserGroup(Guid userGroupId, Guid roleId) {
      UserGroupId = userGroupId;
      RoleId = roleId;
    }
  }
}