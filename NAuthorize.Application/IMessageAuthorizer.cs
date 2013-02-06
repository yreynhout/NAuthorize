namespace NAuthorize.Application {
  public interface IMessageAuthorizer {
    void Authorize(UserId user, object message);
  }
}