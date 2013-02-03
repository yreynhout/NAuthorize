namespace NAuthorize.Application {
  public static class Extensions {
    public static IHandle<SecurityEnvelope<TMessage>> Secure<TMessage>(
      this IHandle<TMessage> handler, IMessageAuthorizer authorizer) {
      return new SecurityEnvelopeHandler<TMessage>(handler, authorizer);
    }
  }
}