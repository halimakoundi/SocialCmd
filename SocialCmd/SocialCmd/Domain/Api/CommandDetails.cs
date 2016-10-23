namespace SocialCmd.Domain.Api
{
    public class CommandDetails
    {
        private CmdKey _commandKey;
        private string _userName;
        private string _message;
        private string _userNameToFollow;

        public CommandDetails(CmdKey commandKey, string userName, string message, string userNameToFollow)
        {
            _commandKey = commandKey;
            _userName = userName;
            _message = message;
            _userNameToFollow = userNameToFollow;
        }

        public CmdKey CommandKey
        {
            get { return _commandKey; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Message
        {
            get { return _message; }
        }

        public string UserNameToFollow
        {
            get { return _userNameToFollow; }
        }
    }
}