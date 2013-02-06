using System;

namespace NAuthorize.Messaging.Commands {
  public class DisableUser {
    public readonly Guid UserId;

    public DisableUser(Guid userId) {
      UserId = userId;
    }
  }
}