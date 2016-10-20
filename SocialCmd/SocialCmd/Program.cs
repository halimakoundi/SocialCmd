using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd
{
    class MainClass
    {
        static Dictionary<string, CmdKey> cmdKeys = new Dictionary<string, CmdKey>();

        public static void Main(string[] args)
        {
            try
            {
                InitialiseApplication();
            }
            catch (Exception ex)
            {
                ApplicationError("Initialising application failed. " + Environment.NewLine + ex.Message);
            }
            do
            {
                try
                {
                    var enteredCommand = PromptUserForCommand();
                    var result = ExecuteCommandAndReturnResult(enteredCommand);
                    PrintResult(result);
                }
                catch (Exception ex)
                {
                    ApplicationError(ex.Message);
                }
            } while (true);

        }

        private static void PrintResult(QualifiedBoolean result)
        {
            if (result.Success)
            {
                Console.WriteLine(result.Value);
            }
            else
            {
                ApplicationError(result.Value);
            }
        }

        private static QualifiedBoolean ExecuteCommandAndReturnResult(string enteredCommand)
        {

            return SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, enteredCommand);
        }

        private static string PromptUserForCommand()
        {
            Console.Write("> ");
            return Console.ReadLine().Trim();
        }

        private static void InitialiseApplication()
        {
            cmdKeys = Settings.ValidCommands();
        }

        private static void ApplicationError(String message)
        {
            Console.WriteLine("A problem occured : " + message);
        }

    }
}
