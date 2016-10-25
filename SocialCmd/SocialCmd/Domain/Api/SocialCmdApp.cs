using System;
using System.Collections.Generic;
using SocialCmd.Domain.Model;
using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    public class SocialCmdApp
    {
        private readonly UserRepository _userRepository;

        public SocialCmdApp(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CommandResponse ExecuteCommandAndReturnResult(
            Dictionary<string, CmdKey> cmdKeys,
            string enteredCommand)
        {
            EnsureIsValid(enteredCommand);

            var details = CommadParser.CommandDetailsFrom(cmdKeys, enteredCommand);

            return ExecuteCommandWith(details);
        }

        private CommandResponse ExecuteCommandWith(CommandDetails commandDetails)
        {
            var command = CommandFrom(commandDetails, _userRepository);
            var result = command.Execute();
            return result;
        }

        private static ICommand CommandFrom(CommandDetails commandDetails, UserRepository userRepository)
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

        private static void EnsureIsValid(string enteredCommand)
        {
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
        }
    }
}