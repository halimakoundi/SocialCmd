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
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    var command = new PostCommand(commandDetails, _userRepository);
                    result = command.Execute();
                    break;
                case CmdKey.Follow:
                    result = UserFollowAnotherUser(commandDetails.UserName, commandDetails.UserNameToFollow);
                    break;
                case CmdKey.Read:
                    result = ReadUserPosts(commandDetails.UserName);
                    break;
                case CmdKey.PrintWall:
                    result = PrintUserWall(commandDetails.UserName);
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

        public CommandResponse ReadUserPosts(string userName)
        {
            var result = new CommandResponse();
            User user = _userRepository.FindUserBy(userName);
            result.Success = true;
            if (user != null)
            {
                result.Value = user.Read();
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        public CommandResponse PrintUserWall(string userName)
        {
            var result = new CommandResponse();
            var userExist = _userRepository.FindUserBy(userName);
            if (userExist != null)
            {
                result.Value = userExist.WriteToWall();
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        public CommandResponse UserFollowAnotherUser(string userName, string userNameToFollow)
        {
            var result = new CommandResponse();
            var user = _userRepository.FindUserBy(userName);

            var usertoFollow = _userRepository.FindUserBy(userNameToFollow);

            if (user != null && usertoFollow != null)
            {
                user.Follow(usertoFollow);
            }
            else
            {
                result.Value = $"User{(usertoFollow != null ? " to follow" : "")} does not exist";
                result.Success = false;
            }
            return result;
        }
    }
}