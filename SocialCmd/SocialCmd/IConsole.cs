﻿namespace SocialCmd
{
    public interface IConsole
    {
        string ReadUserInput();
        void PrintError(string message);
    }
}