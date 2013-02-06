using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUser {
    public readonly Guid UserId;
    public readonly Uri Identifier;

    public AddUser(Guid userId, Uri identifier) {
      UserId = userId;
      Identifier = identifier;
    }
  }
}