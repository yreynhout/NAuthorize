using System;

namespace NAuthorize.Messaging.Events {
  public class DisabledUser {
    public readonly Guid UserId;

    public DisabledUser(Guid userId) {
      UserId = userId;
    }
  }
}