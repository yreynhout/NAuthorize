namespace NAuthorize.Application {
  public static class Extensions {
    public static IHandle<AuthorizationEnvelope<TMessage>> Authorize<TMessage>(
      this IHandle<TMessage> handler, IMessageAuthorizer authorizer) {
      return new AuthorizationEnvelopeHandler<TMessage>(handler, authorizer);
    }
  }
}