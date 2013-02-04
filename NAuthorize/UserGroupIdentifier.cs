using System;

namespace NAuthorize {
  public struct UserGroupIdentifier {
    readonly Uri _value;

    public UserGroupIdentifier(Uri value) {
      if (value == null)
        throw new ArgumentNullException("value");
      _value = value;
    }

    public static implicit operator Uri(UserGroupIdentifier identifier) {
      return identifier._value;
    }
  }
}