using System;
using ProtoBuf;

namespace NAuthorize.Messaging.Events {
  [ProtoContract]
  public class AddedUser {
    [ProtoMember(1)]
    public readonly Guid UserId;
    [ProtoMember(2)]
    public readonly Uri Identifier;

    public AddedUser(Guid userId, Uri identifier) {
      UserId = userId;
      Identifier = identifier;
    }
  }
}