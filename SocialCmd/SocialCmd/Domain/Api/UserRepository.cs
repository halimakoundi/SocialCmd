using System.Collections.Generic;
using SocialCmd.Domain.Model;

namespace SocialCmd.Domain.Api
{
    public class UserRepository
    {
        public User UserBy(string userName)
        {
            User user;
            AppUsers.TryGetValue(userName, out user);

            return user;
        }

        public User CreateUserWith(string userName)
        {
            if (AppUsers.ContainsKey(userName)) return null;
            var user = new User(userName.ToLower());
            AppUsers.Add(user.UserName, user);
            return user;
        }

        public static readonly Dictionary<string, User> AppUsers = new Dictionary<string, User>();
    }
}