using System;
using System.Collections.Generic;

namespace NAuthorize.Tests.Infrastructure {
  public class MemoryEventStoreReader {
    readonly Dictionary<Guid, List<object>> _storage;

    public MemoryEventStoreReader(Dictionary<Guid, List<object>> storage) {
      if (storage == null) throw new ArgumentNullException("storage");
      _storage = storage;
    }

    public Tuple<int, IEnumerable<object>> Read(Guid id) {
      List<object> events;
      if (_storage.TryGetValue(id, out events)) {
        return new Tuple<int, IEnumerable<object>>(events.Count, events);
      }
      return null;
    }
  }
}