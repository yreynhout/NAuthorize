using System;
using NAuthorize.Messaging.Commands;

namespace NAuthorize.Importer {
  public class Record {
    readonly string _name;
    readonly string _identifier;

    Record(string name, string identifier) {
      _name = name;
      _identifier = identifier;
    }

    public static bool TryParse(string row, out Record record) {
      var columns = row.Split('|');
      if (columns.Length != 2) {
        record = null;
        return false;
      }
      record = new Record(columns[0], columns[1]);
      return true;
    }

    public AddUser ToCommand() {
      return new AddUser(Guid.NewGuid(),new Uri(string.Format("urn:windows:sid:{0}", _identifier)));
    }
  }
}