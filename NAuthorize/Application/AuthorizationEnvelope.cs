namespace NAuthorize.Application {
  public class AuthorizationEnvelope<TMessage> {
    readonly TMessage _message;
    readonly UserId _userId;

    public AuthorizationEnvelope(UserId userId, TMessage message) {
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