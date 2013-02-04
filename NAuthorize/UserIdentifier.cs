using System;

namespace NAuthorize {
  public struct UserIdentifier {
    readonly Uri _value;

    public UserIdentifier(Uri value) {
      if (value == null) 
        throw new ArgumentNullException("value");
      _value = value;
    }

    public static implicit operator Uri(UserIdentifier identifier) {
      return identifier._value;
    }
  }
}