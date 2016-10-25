using SocialCmd.Domain.Model;
using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    internal class ReadAllPostsCommand: ICommand
    {
        private readonly UserRepository _userRepository;
        private readonly CommandDetails _commandDetails;

        public ReadAllPostsCommand(CommandDetails commandDetails, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _commandDetails = commandDetails;
        }

        public CommandResponse Execute()
        {
            var result = new CommandResponse();
            var user = _userRepository.FindUserBy(_commandDetails.UserName);
            if (user != null)
            {
                result.Value = Printer.Read(user);
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
    }
}