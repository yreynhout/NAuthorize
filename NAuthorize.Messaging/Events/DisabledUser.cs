using System;

namespace NAuthorize.Messaging.Events {
  public class DisabledUser {
    public readonly Guid UserId;

    public DisabledUser(Guid userId) {
      UserId = userId;
    }

    public override string ToString() {
      return string.Format("Disabled user with id {0}", UserId);
    }
  }
}