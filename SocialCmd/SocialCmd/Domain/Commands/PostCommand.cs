
using SocialCmd.Domain.Model;
using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    internal class PostCommand : ICommand
    {
        private readonly CommandDetails _commandDetails;
        private readonly UserRepository _userRepository;

        public PostCommand(CommandDetails commandDetails, UserRepository userRepository)
        {
            _commandDetails = commandDetails;
            _userRepository = userRepository;
        }

        public CommandResponse Execute()
        {
            var user =_userRepository.FindUserBy(_commandDetails.UserName) 
                     ?? _userRepository.CreateUserWith(_commandDetails.UserName);
            user.Post(_commandDetails.Message);

            return new CommandResponse();
        }
    }
}