using System;
using System.Collections.Generic;
using SocialCmd.Domain.Api;

namespace SocialCmd
{
    class MainClass
    {
        static Dictionary<string, CmdKey> _cmdKeys = new Dictionary<string, CmdKey>();
        private static readonly Console _console = new Console();
        private static readonly UserRepository UserRepository = new UserRepository();

        public static void Main(string[] args)
        {
            SetUpApplication();
            while (true) 
            {
                RunApplication();
            };
        }

        private static void RunApplication()
        {
            try
            {
                var enteredCommand = _console.PromptUserForCommand();
                var result = new SocialCmdApp(UserRepository).ExecuteCommandAndReturnResult(_cmdKeys, enteredCommand);
                _console.PrintResult(result);
            }
            catch (Exception ex)
            {
                _console.PrintError(ex.Message);
            }
        }

        private static void SetUpApplication()
        {
            try
            {
                _cmdKeys = Settings.ValidCommands();
            }
            catch (Exception ex)
            {
                _console.PrintError("Initialising application failed. " + Environment.NewLine + ex.Message);
            }
        }
    }
}
