using System;

namespace NAuthorize {
  public struct PermissionId : IEquatable<PermissionId> {
    public static readonly PermissionId None = new PermissionId();

    public static bool operator ==(PermissionId left, PermissionId right) {
      return left.Equals(right);
    }

    public static bool operator !=(PermissionId left, PermissionId right) {
      return !left.Equals(right);
    }

    public bool Equals(PermissionId other) {
      return ReferenceEquals(_value, null) ? 
        ReferenceEquals(other._value, null) :
        _value.Equals(other._value);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      return obj is PermissionId && Equals((PermissionId) obj);
    }

    public override int GetHashCode() {
      return ReferenceEquals(_value, null) ? 0 : _value.GetHashCode();
    }

    readonly Uri _value;

    public PermissionId(Uri value) {
      if (value == null) throw new ArgumentNullException("value");
      _value = value;
    }

    public static implicit operator Uri(PermissionId id) {
      return id._value;
    }

    public override string ToString() {
      return String.Format("Permission/{0}", _value);
    }
  }
}