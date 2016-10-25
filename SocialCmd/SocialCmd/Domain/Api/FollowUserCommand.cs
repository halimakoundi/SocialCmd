using System;
using SocialCmd.Domain.Model;
using SocialCmd.Domain.Utilities;

namespace SocialCmd.Domain.Api
{
    internal class FollowUserCommand : ICommand
    {
        private readonly CommandDetails _commandDetails;
        private readonly UserRepository _userRepository;

        public FollowUserCommand(CommandDetails commandDetails, UserRepository userRepository)
        {
            _commandDetails = commandDetails;
            _userRepository = userRepository;
        }

        public CommandResponse Execute()
        {
            var usertoFollow = _userRepository.FindUserBy(_commandDetails.UserNameToFollow);
            var user = _userRepository.FindUserBy(_commandDetails.UserName);

            var result = UserFollow(usertoFollow, user);

            return result;
        }

        private static CommandResponse UserFollow(User usertoFollow, User user)
        {
            var result = new CommandResponse();
            if (user != null && usertoFollow != null)
            {
                user.Follow(usertoFollow);
            }
            else
            {
                result = InvalidRequestFor(usertoFollow);
            }
            return result;
        }

        private static CommandResponse InvalidRequestFor(User usertoFollow)
        {
            var result = new CommandResponse
            {
                Value = $"User{(usertoFollow != null ? " to follow" : "")} does not exist",
                Success = false
            };
            return result;
        }
    }
}