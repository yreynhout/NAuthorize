using System;

namespace NAuthorize.Messaging.Commands {
  public class DisableUser {
    public readonly Guid UserId;

    public DisableUser(Guid userId) {
      UserId = userId;
    }

    public override string ToString() {
      return string.Format("Disabling user with id {0}", UserId);
    }
  }
}