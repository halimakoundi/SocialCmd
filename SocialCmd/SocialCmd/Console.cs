using System;

namespace SocialCmd
{
    class Console:IConsole
    {
        public string PromptUserForCommand()
        {
            System.Console.Write("> ");
            return System.Console.ReadLine().Trim();
        }

        public void PrintLine(String message)
        {
            System.Console.WriteLine("A problem occured : " + message);
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }


        public void PrintResult(QualifiedBoolean result)
        {
            if (result.Success)
            {
                System.Console.WriteLine(result.Value);
            }
            else
            {
                PrintLine(result.Value);
            }
        }
    }
}