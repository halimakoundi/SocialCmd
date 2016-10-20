using System;
using System.Collections.Generic;

namespace SocialCmd
{
    class MainClass
    {
        static Dictionary<string, CmdKey> cmdKeys = new Dictionary<string, CmdKey>();
        private static Console _console;

        public static void Main(string[] args)
        {
            try
            {
                cmdKeys = Settings.ValidCommands();
            }
            catch (Exception ex)
            {
                _console.PrintLine("Initialising application failed. " + Environment.NewLine + ex.Message);
            }
            do
            {
                try
                {
                    _console = new Console();
                    var enteredCommand = _console.PromptUserForCommand();
                    var result = ExecuteCommandAndReturnResult(enteredCommand);
                    _console.PrintResult(result);
                }
                catch (Exception ex)
                {
                    _console.PrintLine(ex.Message);
                }
            } while (true);

        }

        private static QualifiedBoolean ExecuteCommandAndReturnResult(string enteredCommand)
        {

            return SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, enteredCommand);
        }
    }
}
