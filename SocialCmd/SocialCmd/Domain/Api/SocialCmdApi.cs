using System;
using System.Collections.Generic;

namespace SocialCmd
{
    public class SocialCmdApi
    {
        private static readonly Dictionary<string, User> appUsers = new Dictionary<string, User>();
        private readonly IConsole _console;
        private readonly CommandParser _parser;

        public SocialCmdApi(CommandParser parser, IConsole console)
        {
            _parser = parser;
            _console = console;
        }

        public static QualifiedBoolean ExecuteCommandAndReturnResult(
            Dictionary<string, CmdKey> cmdKeys,
            string enteredCommand)
        {
            var result = new QualifiedBoolean();
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
            //getting the number of words in the entered command considering the user name cannot contain spaces				
            var commandParts = enteredCommand.Trim().Split(' ');
            var userName = commandParts[0].ToLower();
            var key = string.Empty;
            CmdKey currentKey = 0;

            if (commandParts.Length == 1)
            {
                //Here only the username has been typed - read posts for that user
                currentKey = CmdKey.Read;
            }
            if (commandParts.Length >= 2)
            {
                key = commandParts[1].ToLower();
                cmdKeys.TryGetValue(key, out currentKey);
            }
            switch (currentKey)
            {
                case CmdKey.Post:
                    var message = enteredCommand.Split(new[]{key}, StringSplitOptions.None)[1];
                    if (!string.IsNullOrEmpty(message))
                    {
                        result = PostMessageToUser(userName, message);
                    }
                    break;
                case CmdKey.Follow:
                    var userNameToFollow = commandParts[2].ToLower();
                    result = UserFollowAnotherUser(userName, userNameToFollow);
                    break;
                case CmdKey.Read:
                    result = ReadUserPosts(userName);
                    break;
                case CmdKey.PrintWall:
                    result = PrintUserWall(userName);
                    break;
                default:
                    result.Value = "Command not recognised.";
                    result.Success = false;
                    break;
            }
            return result;
        }

        public static QualifiedBoolean PostMessageToUser(string userName, string message)
        {
            var result = new QualifiedBoolean();
            User user;
            UserExists(userName, out user, out result, true);
            if (user != null)
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