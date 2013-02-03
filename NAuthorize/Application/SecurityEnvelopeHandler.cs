using System;

namespace NAuthorize.Application {
  public class SecurityEnvelopeHandler<TMessage> : IHandle<SecurityEnvelope<TMessage>> {
    readonly IMessageAuthorizer _authorizer;
    readonly IHandle<TMessage> _handler;

    public SecurityEnvelopeHandler(IHandle<TMessage> handler, IMessageAuthorizer authorizer) {
      if (authorizer == null) throw new ArgumentNullException("authorizer");
      if (handler == null) throw new ArgumentNullException("handler");
      _authorizer = authorizer;
      _handler = handler;
    }

    public void Handle(SecurityEnvelope<TMessage> envelope) {
      _authorizer.Authorize(envelope.UserId, envelope.Message);
      _handler.Handle(envelope.Message);
    }
  }
}