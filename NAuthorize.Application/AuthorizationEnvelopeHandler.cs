using System;

namespace NAuthorize.Application {
  public class AuthorizationEnvelopeHandler<TMessage> : IHandle<AuthorizationEnvelope<TMessage>> {
    readonly IMessageAuthorizer _authorizer;
    readonly IHandle<TMessage> _handler;

    public AuthorizationEnvelopeHandler(IHandle<TMessage> handler, IMessageAuthorizer authorizer) {
      if (authorizer == null) throw new ArgumentNullException("authorizer");
      if (handler == null) throw new ArgumentNullException("handler");
      _authorizer = authorizer;
      _handler = handler;
    }

    public void Handle(AuthorizationEnvelope<TMessage> envelope) {
      _authorizer.Authorize(envelope.UserId, envelope.Message);
      _handler.Handle(envelope.Message);
    }
  }
}