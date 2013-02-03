using System;

namespace NAuthorize.Messaging.Events {
  public class RevokedRoleFromUserGroup {
    public readonly Guid UserGroupId;
    public readonly Guid RoleId;

    public RevokedRoleFromUserGroup(Guid userGroupId, Guid roleId) {
      UserGroupId = userGroupId;
      RoleId = roleId;
    }
  }
}