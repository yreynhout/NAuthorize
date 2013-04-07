using System;
using System.Collections.Generic;
using AggregateSource;
using StreamSource;

namespace NAuthorize.Tests.Infrastructure {
  public class MemoryEventStoreReader : IEventStreamReader {
    readonly Dictionary<string, List<object>> _storage;

    public MemoryEventStoreReader(Dictionary<string, List<object>> storage) {
      if (storage == null) throw new ArgumentNullException("storage");
      _storage = storage;
    }
    
    public Optional<EventStream> Read(Guid id) {
      List<object> events;
      return 
        _storage.TryGetValue(id.ToString("N"), out events) ? 
        new Optional<EventStream>(new EventStream(events.Count, events)) : 
        Optional<EventStream>.Empty;
    }
  }
}