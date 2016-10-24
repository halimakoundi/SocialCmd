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
            var userExist = _userRepository.FindUserBy(_commandDetails.UserName);
            if (userExist != null)
            {
                result.Value = userExist.WriteToWall();
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
    }
}