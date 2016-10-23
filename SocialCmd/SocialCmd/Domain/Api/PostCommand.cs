
namespace SocialCmd.Domain.Api
{
    internal class PostCommand : ISocialCommand
    {
        private readonly string _userName;
        private readonly string _message;
        private UserRepository _userRepository;

        public PostCommand(string userName, string message, UserRepository userRepository)
        {
            _userName = userName;
            _message = message;
            _userRepository = userRepository;
        }

        public QualifiedBoolean PostMessageToUser()
        {
            User user;
            QualifiedBoolean result;
            _userRepository.UserExists(_userName, out user, out result, true);

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