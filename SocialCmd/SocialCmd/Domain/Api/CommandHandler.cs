namespace SocialCmd.Domain.Api
{
    public class CommandHandler
    {
        public CommandHandler(string message, string userName)
        {
            Message = message;
            UserName = userName;
        }

        public CommandHandler()
        {
        }

        public string UserName { get; }

        public string Message { get; }

        public QualifiedBoolean Execute(ICommand command)
        {
            return command.Execute(this);
        }
    }
}