using System;
using ProtoBuf;

namespace NAuthorize.Messaging.Events {
  [ProtoContract]
  public class AddedPermissionToRole {
    [ProtoMember(1)]
    public readonly Guid RoleId;
    [ProtoMember(2)]
    public readonly Uri PermissionId;

    public AddedPermissionToRole(Guid roleId, Uri permissionId) {
      RoleId = roleId;
      PermissionId = permissionId;
    }
  }
}