
using SocialCmd.Domain.Model;

namespace SocialCmd.Domain.Api
{
    internal class PostCommand : ISocialCommand
    {
        private readonly CommandDetails _commandDetails;
        private readonly UserRepository _userRepository;

        public PostCommand(CommandDetails commandDetails, UserRepository userRepository)
        {
            _commandDetails = commandDetails;
            _userRepository = userRepository;
        }

        public QualifiedBoolean Execute()
        {
            var user =_userRepository.FindUserBy(_commandDetails.UserName) 
                     ?? _userRepository.CreateUserWith(_commandDetails.UserName);
            user.Post(_commandDetails.Message);

            return new QualifiedBoolean();
        }
    }

    public interface ISocialCommand
    {
    }
}