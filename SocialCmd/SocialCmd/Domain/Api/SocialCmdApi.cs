using System;
using System.Collections.Generic;

namespace SocialCmd.Domain.Api
{
    public class SocialCmdApi
    {
        public static readonly Dictionary<string, User> AppUsers = new Dictionary<string, User>();
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
                    var command = new PostCommand(commandDetails.UserName, commandDetails.Message);
                    result = command.PostMessageToUser();
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

        public  QualifiedBoolean ReadUserPosts(string userName)
        {
            var result = new QualifiedBoolean();
            User user;
            _userRepository.UserExists(userName, out user, out result);
            if (user != null)
            {
                result.Value = user.Read();
            }
            return result;
        }

        public QualifiedBoolean PrintUserWall(string userName)
        {
            var result = new QualifiedBoolean();
            User user;
            var userExist = _userRepository.UserExists(userName, out user, out result);
            if (userExist && user != null)
            {
                result.Value = user.WriteToWall();
            }
            return result;
        }

        public  QualifiedBoolean UserFollowAnotherUser(string userName, string userNameToFollow)
        {
            QualifiedBoolean result;
            User user;
            var userExist = _userRepository.UserExists(userName, out user, out result);

            User userToFollow;
            var usertoFollowExist = _userRepository.UserExists(userNameToFollow, out userToFollow, out result);

            if (userExist && usertoFollowExist && user != null && userToFollow != null)
            {
                user.Follow(userToFollow);
            }
            else
            {
                result.Value = $"User{(!usertoFollowExist ? " to follow" : "")} does not exist";
                result.Success = false;
            }
            return result;
        }
    }
}