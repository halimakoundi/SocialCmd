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
                    result = UserFollowAnotherUser(commandDetails.UserName, commandDetails.UserNameToFollow, _userRepository);
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

        public static CommandResponse UserFollowAnotherUser(string userName, string userNameToFollow, UserRepository userRepository)
        {
            var usertoFollow = userRepository.FindUserBy(userNameToFollow);
            var user = userRepository.FindUserBy(userName);

            var result = UserFollow(usertoFollow, user);

            return result;
        }

        private static CommandResponse UserFollow(User usertoFollow, User user)
        {
            var result = new CommandResponse();
            if (user != null && usertoFollow != null)
            {
                user.Follow(usertoFollow);
            }
            else
            {
                result = InvalidRequestFor(usertoFollow);
            }
            return result;
        }

        private static CommandResponse InvalidRequestFor(User usertoFollow)
        {
            var result = new CommandResponse
            {
                Value = $"User{(usertoFollow != null ? " to follow" : "")} does not exist",
                Success = false
            };
            return result;
        }
    }
}