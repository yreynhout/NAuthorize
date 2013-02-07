using System;
using NAuthorize.Application;

namespace NAuthorize.Tests {
  public class HandlerAdapter<T> : IHandle<object> {
    readonly IHandle<T> _handler;

    public HandlerAdapter(IHandle<T> handler) {
      if (handler == null) throw new ArgumentNullException("handler");
      _handler = handler;
    }

    public void Handle(object message) {
      if(message is T)
        _handler.Handle((T) message);
    }
  }
}