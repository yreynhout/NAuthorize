using System;

namespace NAuthorize.Messaging.Events {
  public class AddedUser {
    public readonly Guid UserId;
    public readonly Uri Identifier;

    public AddedUser(Guid userId, Uri identifier) {
      UserId = userId;
      Identifier = identifier;
    }
  }
}