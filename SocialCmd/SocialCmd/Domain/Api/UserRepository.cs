using System.Collections.Generic;
using SocialCmd.Domain.Model;

namespace SocialCmd.Domain.Api
{
    public class UserRepository
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();

        public User UserBy(string userName)
        {
            User user;
            _users.TryGetValue(userName, out user);

            return user;
        }

        public User CreateUserWith(string userName)
        {
            if (_users.ContainsKey(userName)) return null;
            var user = new User(userName.ToLower());
            _users.Add(user.UserName, user);
            return user;
        }
    }
}