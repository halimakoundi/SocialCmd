
using SocialCmd.Domain.Model;

namespace SocialCmd.Domain.Api
{
    internal class PostCommand : ISocialCommand
    {
        private readonly string _userName;
        private readonly string _message;
        private readonly UserRepository _userRepository;

        public PostCommand(string userName, string message, UserRepository userRepository)
        {
            _userName = userName;
            _message = message;
            _userRepository = userRepository;
        }

        public QualifiedBoolean Execute()
        {
            var user =_userRepository.UserBy(_userName) 
                     ?? _userRepository.CreateUserWith(_userName);
            user.Post(_message);

            return new QualifiedBoolean();
        }
    }

    public interface ISocialCommand
    {
    }
}