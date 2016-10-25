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
            ICommand command;
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    command = new PostCommand(commandDetails, _userRepository);
                    result = command.Execute();
                    break;
                case CmdKey.Follow:
                    command = new FollowUserCommand(commandDetails, _userRepository);
                    result = command.Execute();
                    break;
                case CmdKey.Read:
                    command = new ReadAllPostsCommand(commandDetails, _userRepository);
                    result = command.Execute();
                    break;
                case CmdKey.PrintWall:
                    command = new PrintWallCommand(commandDetails, _userRepository);
                    result = command.Execute();
                    break;
                default:
                    result = SkipInvalidCommand();
                    break;
            }
            return result;
        }

        private static CommandResponse SkipInvalidCommand()
        {
            return new CommandResponse
            {
                Value = "Command not recognised.",
                Success = false
            };
        }

        private static void EnsureIsValid(string enteredCommand)
        {
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
        }
    }
}