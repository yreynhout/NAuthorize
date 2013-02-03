namespace NAuthorize.Application {
  public interface IMessageAuthorizer {
    void Authorize(UserId account, object message);
  }
}