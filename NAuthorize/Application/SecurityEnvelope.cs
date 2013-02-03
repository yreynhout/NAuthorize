namespace NAuthorize.Application {
  public class SecurityEnvelope<TMessage> {
    readonly TMessage _message;
    readonly UserId _userId;

    public SecurityEnvelope(UserId userId, TMessage message) {
      _message = message;
      _userId = userId;
    }

    public TMessage Message {
      get { return _message; }
    }

    public UserId UserId {
      get { return _userId; }
    }
  }
}