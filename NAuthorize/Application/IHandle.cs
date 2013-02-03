namespace NAuthorize.Application {
  public interface IHandle<in TMessage> {
    void Handle(TMessage message);
  }
}