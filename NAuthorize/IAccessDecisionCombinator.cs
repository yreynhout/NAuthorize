namespace NAuthorize {
  public interface IAccessDecisionCombinator {
    IAccessDecisionCombinator CombineDecision(PermissionId permissionId, AccessDecision decision);
    IAccessDecider BuildDecider();
  }
}