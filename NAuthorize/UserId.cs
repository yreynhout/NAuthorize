using System;

namespace NAuthorize {
  public struct UserId : IEquatable<UserId> {
    readonly Guid _value;

    public UserId(Guid value) {
      _value = value;
    }

    public static bool operator ==(UserId left, UserId right) {
      return left.Equals(right);
    }

    public static bool operator !=(UserId left, UserId right) {
      return !left.Equals(right);
    }

    public bool Equals(UserId other) {
      return _value.Equals(other._value);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is UserId && Equals((UserId) obj);
    }

    public override int GetHashCode() {
      return _value.GetHashCode();
    }

    public static implicit operator Guid(UserId id) {
      return id._value;
    }

    public override string ToString() {
      return String.Format("User/{0}", _value.ToString().ToUpperInvariant());
    }
  }
}