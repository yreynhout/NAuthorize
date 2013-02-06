using System;
using ProtoBuf;

namespace NAuthorize.Messaging.Events {
  [ProtoContract]
  public class AddedRole {
    [ProtoMember(1)]
    public readonly Guid RoleId;
    [ProtoMember(2)]
    public readonly string Name;

    public AddedRole(Guid roleId, string name) {
      RoleId = roleId;
      Name = name;
    }
  }
}