using System;
using System.Collections.Generic;

namespace SocialCmd.Domain.Api
{
    internal class CommadParser
    {
        public static CommandDetails CommandDetailsFrom(Dictionary<string, CmdKey> cmdKeys, string enteredCommand)
        {
            var commandParts = CommandPartsFrom(enteredCommand);
            var userName = UserNameFrom(commandParts);
            var userNameToFollow = UserNameToFollowFrom(commandParts);

            var key = KeyFrom(commandParts);
            var commandKey = CommandKeyFrom(cmdKeys, commandParts, key);
            var message = MessageFrom(enteredCommand, key);

            var commandDetails = new CommandDetails(commandKey, userName, message, userNameToFollow);
            return commandDetails;
        }

        private static string MessageFrom(string enteredCommand, string key)
        {
            var userInputs = enteredCommand.Split(new[] { key }, StringSplitOptions.None);
            return userInputs.Length > 1 
                ? userInputs[1] 
                : String.Empty;
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

        private static bool IsReadCommand(string[] commandParts)
        {
            return commandParts.Length == 1;
        }

        private static string KeyFrom(string[] commandParts)
        {
            return commandParts.Length >= 2
                ? commandParts[1].ToLower()
                : String.Empty;
        }

        private static string UserNameToFollowFrom(string[] commandParts)
        {
            return commandParts.Length > 2 ?
                commandParts[2].ToLower() : String.Empty;
        }

        private static string UserNameFrom(string[] commandParts)
        {
            return commandParts[0].ToLower();
        }

        private static string[] CommandPartsFrom(string enteredCommand)
        {
            return enteredCommand.Trim().Split(' ');
        }
    }
}