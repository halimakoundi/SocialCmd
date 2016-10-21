using SocialCmd.Domain.Api;

namespace SocialCmd
{
    public class PostCommand : ICommand
    {
        private readonly string _details;

        public PostCommand(string details)
        {
            _details = details;
        }

        public static ICommand With(string details)
        {
            return new PostCommand(details);
        }

        public QualifiedBoolean Execute(CommandHandler handler)
        {
            return  PostMessage(handler);
        }

        public QualifiedBoolean PostMessage(CommandHandler handler)
        {
            return SocialCmdApi.PostMessageToUser(handler.UserName, handler.Message); ;
        }
    }
}