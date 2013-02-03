using System;

namespace NAuthorize.Messaging.Events {
  public class GrantedRoleToUserGroup {
    public readonly Guid UserGroupId;
    public readonly Guid RoleId;

    public GrantedRoleToUserGroup(Guid userGroupId, Guid roleId) {
      UserGroupId = userGroupId;
      RoleId = roleId;
    }
  }
}