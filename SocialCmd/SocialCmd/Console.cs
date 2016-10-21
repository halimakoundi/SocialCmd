using System;

namespace SocialCmd
{
    class Console : IConsole
    {
        private const string PROMPT = "> ";
        private const string A_PROBLEM_OCCURED = "A problem occured : ";

        public string PromptUserForCommand()
        {
            Write(PROMPT);
            return ReadUserInput();
        }

        public string ReadUserInput()
        {
            var userInput = System.Console.ReadLine();
            return userInput?.Trim() ?? string.Empty;
        }

        public void PrintError(string message)
        {
            Write(A_PROBLEM_OCCURED + message);
        }

        public void PrintResult(QualifiedBoolean result)
        {
            if (result.Success)
            {
                Write(result.Value);
            }
            else
            {
                PrintError(result.Value);
            }
        }

        private static void Write(string message)
        {
            System.Console.Write(message);
        }
    }
}