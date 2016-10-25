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
            CommandResponse result;
            ICommand command = null;
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    command = new PostCommand(commandDetails, _userRepository);
                    break;
                case CmdKey.Follow:
                    command = new FollowUserCommand(commandDetails, _userRepository);
                    break;
                case CmdKey.Read:
                    command = new ReadAllPostsCommand(commandDetails, _userRepository);
                    break;
                case CmdKey.PrintWall:
                    command = new PrintWallCommand(commandDetails, _userRepository);
                    break;
                default:
                    command = new InvalidCommand();
                    break;
            }
            result = command.Execute();
            return result;
        }

        private static void EnsureIsValid(string enteredCommand)
        {
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
        }
    }
}