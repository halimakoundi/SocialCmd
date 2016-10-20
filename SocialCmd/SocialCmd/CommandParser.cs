using System;

namespace SocialCmd
{
    public class CommandParser

    {
        public virtual ICommand Parse(string command)
        {
            var details = command.Split(new []{"->"},StringSplitOptions.RemoveEmptyEntries)[1];
            return PostCommand.With(details);
        }
    }

    public class PostCommand : ICommand
    {
        private readonly string _details;

        public PostCommand(string details)
        {
            _details = details;
        }

        public static ICommand With(string details)
        {
           return new PostCommand(details);
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}