using SocialCmd.Domain.Model;
using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    internal class PrintWallCommand : ICommand
    {
        private readonly CommandDetails _commandDetails;
        private readonly UserRepository _userRepository;

        public PrintWallCommand(CommandDetails commandDetails, UserRepository userRepository)
        {
            _commandDetails = commandDetails;
            _userRepository = userRepository;
        }

        public CommandResponse Execute()
        {
            var result = new CommandResponse();
            var user = _userRepository.FindUserBy(_commandDetails.UserName);
            if (user != null)
            {
                result.Value = Printer.WriteToWall(user);
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
    }
}