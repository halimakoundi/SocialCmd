namespace SocialCmd.Domain.Api
{
    public class UserRepository
    {
        public bool UserExists(string userName, out User user, out QualifiedBoolean result,
            bool createIfNotExist = false)
        {
            result = new QualifiedBoolean();
            var userExist = SocialCmdApi.AppUsers.TryGetValue(userName, out user);
            if (!userExist && createIfNotExist)
            {
                user = new User(userName.ToLower());
                SocialCmdApi.AppUsers.Add(user.UserName, user);
            }
            else if (!userExist)
            {
                result.Value = "User does not exist.";
                result.Success = false;
            }
            return userExist;
        }
    }
}