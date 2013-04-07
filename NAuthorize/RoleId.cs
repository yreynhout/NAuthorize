using System;

namespace NAuthorize {
  public struct RoleId : IEquatable<RoleId> {
    readonly Guid _value;

    public RoleId(Guid value) {
      _value = value;
    }

    public static bool operator ==(RoleId left, RoleId right) {
      return left.Equals(right);
    }

    public static bool operator !=(RoleId left, RoleId right) {
      return !left.Equals(right);
    }

    public bool Equals(RoleId other) {
      return _value.Equals(other._value);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is RoleId && Equals((RoleId) obj);
    }

    public override int GetHashCode() {
      return _value.GetHashCode();
    }

    public static implicit operator Guid(RoleId id) {
      return id._value;
    }

    public static implicit operator String(RoleId id) {
      return id._value.ToString("N");
      //return id.ToString();
    }

    public override string ToString() {
      return String.Format("Role/{0}", _value.ToString().ToUpperInvariant());
    }
  }
}