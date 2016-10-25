namespace SocialCmd.Domain.Api
{
    public class CommandDetails
    {
        public CommandDetails(CmdKey commandKey, string userName, string message, string userNameToFollow)
        {
            CommandKey = commandKey;
            UserName = userName;
            Message = message;
            UserNameToFollow = userNameToFollow;
        }

        public CmdKey CommandKey { get; }

        public string UserName { get; }

        public string Message { get; }

        public string UserNameToFollow { get; }
    }
}