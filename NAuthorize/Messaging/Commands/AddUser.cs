using System;

namespace NAuthorize.Messaging.Commands {
  public class AddUser {
    public readonly Guid UserId;
    public readonly string Name;

    public AddUser(Guid userId, string name) {
      UserId = userId;
      Name = name;
    }
  }
}