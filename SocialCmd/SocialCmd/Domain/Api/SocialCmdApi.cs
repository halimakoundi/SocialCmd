using System;
using System.Collections.Generic;
using SocialCmd.Domain.Model;

namespace SocialCmd.Domain.Api
{
    public class SocialCmdApi
    {
        private readonly UserRepository _userRepository;

        public SocialCmdApi(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public QualifiedBoolean ExecuteCommandAndReturnResult(
            Dictionary<string, CmdKey> cmdKeys,
            string enteredCommand)
        {
            EnsureIsValid(enteredCommand);

            var details = CommadParser.CommandDetailsFrom(cmdKeys, enteredCommand);

            return ExecuteCommandWith(details);
        }

        private QualifiedBoolean ExecuteCommandWith(CommandDetails commandDetails)
        {
            QualifiedBoolean result;
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    var command = new PostCommand(commandDetails.UserName, commandDetails.Message, _userRepository);
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

        private static QualifiedBoolean SkipInvalidCommand()
        {
            return new QualifiedBoolean
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

        public QualifiedBoolean ReadUserPosts(string userName)
        {
            var result = new QualifiedBoolean();
            User user = _userRepository.UserBy(userName);
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

        public QualifiedBoolean PrintUserWall(string userName)
        {
            var result = new QualifiedBoolean();
            var userExist = _userRepository.UserBy(userName);
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

        public QualifiedBoolean UserFollowAnotherUser(string userName, string userNameToFollow)
        {
            var result = new QualifiedBoolean();
            var user = _userRepository.UserBy(userName);

            var usertoFollow = _userRepository.UserBy(userNameToFollow);

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