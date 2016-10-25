namespace SocialCmd.Domain.Api
{
    internal static class Command
    {
        public static ICommand From(CommandDetails commandDetails, UserRepository userRepository)
        {
            ICommand command = null;
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    command = new PostCommand(commandDetails, userRepository);
                    break;
                case CmdKey.Follow:
                    command = new FollowUserCommand(commandDetails, userRepository);
                    break;
                case CmdKey.Read:
                    command = new ReadAllPostsCommand(commandDetails, userRepository);
                    break;
                case CmdKey.PrintWall:
                    command = new PrintWallCommand(commandDetails, userRepository);
                    break;
                default:
                    command = new InvalidCommand();
                    break;
            }
            return command;
        }
    }
}