using System;

namespace NAuthorize {
  public struct UserGroupId : IEquatable<UserGroupId> {
    readonly Guid _value;

    public UserGroupId(Guid value) {
      _value = value;
    }

    public static bool operator ==(UserGroupId left, UserGroupId right) {
      return left.Equals(right);
    }

    public static bool operator !=(UserGroupId left, UserGroupId right) {
      return !left.Equals(right);
    }

    public bool Equals(UserGroupId other) {
      return _value.Equals(other._value);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is UserGroupId && Equals((UserGroupId) obj);
    }

    public override int GetHashCode() {
      return _value.GetHashCode();
    }

    public static implicit operator Guid(UserGroupId id) {
      return id._value;
    }

    public override string ToString() {
      return String.Format("UserGroup/{0}", _value.ToString().ToUpperInvariant());
    }
  }
}