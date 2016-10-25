using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    internal class InvalidCommand : ICommand
    {
        public CommandResponse Execute()
        {
            return new CommandResponse
            {
                Value = "Command not recognised.",
                Success = false
            };
        }
    }
}