using System;
using System.Collections.Generic;
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
            var command = Command.From(commandDetails, _userRepository);
            var result = command.Execute();
            return result;
        }

        private static void EnsureIsValid(string enteredCommand)
        {
            if (string.IsNullOrEmpty(enteredCommand))
                throw new Exception("The command cannot be empty.");
        }
    }
}