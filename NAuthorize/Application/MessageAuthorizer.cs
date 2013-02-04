using System;
using System.Security;
using AggregateSource;

namespace NAuthorize.Application {
  public class MessageAuthorizer : IMessageAuthorizer {
    readonly IPermissionResolver _resolver;
    readonly IRepository<User> _userRepository;
    readonly IRepository<Role> _roleRepository;
    readonly IRepository<UserGroup> _userGroupRepository;

    public MessageAuthorizer(IPermissionResolver resolver,
                             IRepository<User> userRepository,
                             IRepository<Role> roleRepository,
                             IRepository<UserGroup> userGroupRepository) {
      if (resolver == null) throw new ArgumentNullException("resolver");
      if (userRepository == null) throw new ArgumentNullException("userRepository");
      if (roleRepository == null) throw new ArgumentNullException("roleRepository");
      if (userGroupRepository == null) throw new ArgumentNullException("userGroupRepository");
      _resolver = resolver;
      _userRepository = userRepository;
      _roleRepository = roleRepository;
      _userGroupRepository = userGroupRepository;
    }

    public void Authorize(UserId user, object message) {
      var decider = GetDeciderForMessage(user);
      if (!decider.AreAllAllowed(_resolver.ResolvePermission(message))) {
        throw new SecurityException(string.Format("Yo bro, u do not have permission to {0}", message.GetType().Name.ToLowerInvariant()));
      }
    }

    IAccessDecider GetDeciderForMessage(UserId userId) {
      var combinator = new AccessDecisionCombinator();
      var user = _userRepository.Get(userId);
      user.CombineDecisions(combinator, _roleRepository, _userGroupRepository);
      return combinator.BuildDecider();
    }
  }
}