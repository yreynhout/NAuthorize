using System;

namespace NAuthorize.Messaging.Events {
  public class AddedUser {
    public readonly Guid UserId;
    public readonly string Name;

    public AddedUser(Guid userId, string name) {
      UserId = userId;
      Name = name;
    }
  }
}