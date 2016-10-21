using System;
using System.Collections.Generic;

namespace SocialCmd
{
    public class SocialCmdApi
    {
        private static readonly Dictionary<string, User> appUsers = new Dictionary<string, User>();

        public static QualifiedBoolean ExecuteCommandAndReturnResult(
            Dictionary<string, CmdKey> cmdKeys,
            string enteredCommand)
        {
            EnsureIsValid(enteredCommand);

            var commandParts = CommandPartsFrom(enteredCommand);
            var userName = UserNameFrom(commandParts);
            var userNameToFollow = UserNameToFollowFrom(commandParts);

            var key = KeyFrom(commandParts);
            var commandKey = CommandKeyFrom(cmdKeys, commandParts, key);
            var message = MessageFrom(enteredCommand, key);

            return ExecuteCommandBy(commandKey, userName, message, userNameToFollow);
        }

        private static QualifiedBoolean ExecuteCommandBy(CmdKey commandKey, string userName, string message,
            string userNameToFollow)
        {
            var result = new QualifiedBoolean();
            switch (commandKey)
            {
                case CmdKey.Post:
                    result = PostMessageToUser(userName, message);
                    break;
                case CmdKey.Follow:
                    result = UserFollowAnotherUser(userName, userNameToFollow);
                    break;
                case CmdKey.Read:
                    result = ReadUserPosts(userName);
                    break;
                case CmdKey.PrintWall:
                    result = PrintUserWall(userName);
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

        private static string MessageFrom(string enteredCommand, string key)
        {
            var userInputs = enteredCommand.Split(new[] { key }, StringSplitOptions.None);
            return userInputs.Length > 1 
                ? userInputs[1] 
                : string.Empty;
        }

        private static CmdKey CommandKeyFrom(Dictionary<string, CmdKey> cmdKeys, string[] commandParts, string key)
        {
            CmdKey currentKey = 0;
            if (IsReadCommand(commandParts))
            {
                currentKey = CmdKey.Read;
            }
            if (commandParts.Length >= 2)
            {
                cmdKeys.TryGetValue(key, out currentKey);
            }
            return currentKey;
        }

        private static string KeyFrom(string[] commandParts)
        {
            return commandParts.Length >= 2
                ? commandParts[1].ToLower()
                : string.Empty;
        }

        private static bool IsReadCommand(string[] commandParts)
        {
            return commandParts.Length == 1;
        }

        private static string UserNameToFollowFrom(string[] commandParts)
        {
            return commandParts.Length > 2 ?
                commandParts[2].ToLower() : string.Empty;
        }

        private static string UserNameFrom(string[] commandParts)
        {
            return commandParts[0].ToLower();
        }

        private static string[] CommandPartsFrom(string enteredCommand)
        {
            return enteredCommand.Trim().Split(' ');
        }

        private static void EnsureIsValid(string enteredCommand)
        {
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
        }

        public static QualifiedBoolean PostMessageToUser(string userName, string message)
        {
            User user;
            QualifiedBoolean result;
            UserExists(userName, out user, out result, true);

            if ((user == null) || (message == null))
            {
                return result;
            }
            user.Post(message);

            return result;
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
            var userExist = appUsers.TryGetValue(userName, out user);
            if (!userExist && createIfNotExist)
            {
                user = new User(userName.ToLower());
                appUsers.Add(user.UserName, user);
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