
namespace SocialCmd.Domain.Api
{
    internal class PostCommand : ISocialCommand
    {
        private readonly string _userName;
        private readonly string _message;

        public PostCommand(string userName, string message)
        {
            _userName = userName;
            _message = message;
        }

        public QualifiedBoolean PostMessageToUser()
        {
            User user;
            QualifiedBoolean result;
            SocialCmdApi.UserExists(_userName, out user, out result, true);

            if ((user == null) || (_message == null))
            {
                return result;
            }
            user.Post(_message);

            return result;
        }
    }

    public interface ISocialCommand
    {
    }
}