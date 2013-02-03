using System;

namespace NAuthorize {
  public struct UserName {
    readonly string _value;

    public UserName(string value) {
      if (String.IsNullOrEmpty(value))
        throw new ArgumentException("A user name cannot be null or empty.");
      if (value.Length > Metadata.UserNameMaxLength)
        throw new ArgumentException(string.Format("A user name cannot be longer than {0} characters.",
                                                  Metadata.UserNameMaxLength));
      _value = value;
    }

    public static implicit operator string(UserName name) {
      return name._value;
    }
  }
}