using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUser {
    public readonly Guid UserId;
    public readonly Uri Identifier;

    public AddUser(Guid userId, Uri identifier) {
      UserId = userId;
      Identifier = identifier;
    }

    public override string ToString() {
      return string.Format("Adding user {0} with id {1}", Identifier, UserId);
    }
  }
}