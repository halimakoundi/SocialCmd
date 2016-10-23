using System;
using System.Collections.Generic;

namespace SocialCmd.Domain.Api
{
    public class SocialCmdApi
    {
        private static readonly Dictionary<string, User> AppUsers = new Dictionary<string, User>();

        public static QualifiedBoolean ExecuteCommandAndReturnResult(
            Dictionary<string, CmdKey> cmdKeys,
            string enteredCommand)
        {
            EnsureIsValid(enteredCommand);

            var details = CommadParser.CommandDetailsFrom(cmdKeys, enteredCommand);

            return ExecuteCommandWith(details);
        }

        private static QualifiedBoolean ExecuteCommandWith(CommandDetails commandDetails)
        {
            QualifiedBoolean result;
            switch (commandDetails.CommandKey)
            {
                case CmdKey.Post:
                    var command = new PostCommand(commandDetails.UserName, commandDetails.Message);
                    result = command.PostMessageToUser(commandDetails.UserName, commandDetails.Message);
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

        public static QualifiedBoolean ReadUserPosts(string userName)
        {
            var result = new QualifiedBoolean();
            User user;
            UserExists(userName, out user, out result);
            if (user != null)
            {
                result.Value = user.Read();
            }
            return result;
        }

        public static QualifiedBoolean PrintUserWall(string userName)
        {
            var result = new QualifiedBoolean();
            User user;
            var userExist = UserExists(userName, out user, out result);
            if (userExist && user != null)
            {
                result.Value = user.WriteToWall();
            }
            return result;
        }

        public static QualifiedBoolean UserFollowAnotherUser(string userName, string userNameToFollow)
        {
            var result = new QualifiedBoolean();
            User user;
            var userExist = UserExists(userName, out user, out result);

            User userToFollow;
            var usertoFollowExist = UserExists(userNameToFollow, out userToFollow, out result);

            if (userExist && usertoFollowExist && user != null && userToFollow != null)
            {
                user.Follow(userToFollow);
            }
            else
            {
                result.Value = string.Format("User{0} does not exist", !usertoFollowExist ? " to follow" : "");
                result.Success = false;
            }
            return result;
        }

        public static bool UserExists(string userName, out User user, out QualifiedBoolean result,
            bool createIfNotExist = false)
        {
            result = new QualifiedBoolean();
            var userExist = AppUsers.TryGetValue(userName, out user);
            if (!userExist && createIfNotExist)
            {
                user = new User(userName.ToLower());
                AppUsers.Add(user.UserName, user);
            }
            else if (!userExist)
            {
                result.Value = "User does not exist.";
                result.Success = false;
            }
            return userExist;
        }
    }
}