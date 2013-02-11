using System;

namespace NAuthorize.Messaging.Commands {
  public class AddRole {
    public readonly Guid RoleId;
    public readonly string Name;

    public AddRole(Guid roleId, string name) {
      RoleId = roleId;
      Name = name;
    }

    public override string ToString() {
      return string.Format("Adding role {0} with id {1}", Name, RoleId);
    }
  }
}