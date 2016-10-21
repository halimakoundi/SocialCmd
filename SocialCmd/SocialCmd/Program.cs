using System;
using System.Collections.Generic;

namespace SocialCmd
{
    class MainClass
    {
        static Dictionary<string, CmdKey> _cmdKeys = new Dictionary<string, CmdKey>();
        private static readonly Console _console = new Console();

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
                var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, enteredCommand);
                _console.PrintResult(result);
            }
            catch (Exception ex)
            {
                _console.PrintLine(ex.Message);
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
                _console.PrintLine("Initialising application failed. " + Environment.NewLine + ex.Message);
            }
        }
    }
}
