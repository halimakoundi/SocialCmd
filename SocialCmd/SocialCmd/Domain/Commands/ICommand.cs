using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    public interface ICommand
    {
        CommandResponse Execute();
    }
}