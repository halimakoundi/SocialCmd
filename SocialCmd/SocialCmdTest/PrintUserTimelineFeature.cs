using NSubstitute;
using NUnit.Framework;
using SocialCmd;
using SocialCmd.Domain.Api;

namespace SocialCmdTest
{
    [TestFixture]
    public class PrintUserTimelineFeature
    {
        private const string UserName = "Bob";
        private SocialCmdApi _socialCmdApi;
        private CommandParser _parser;
        private IConsole _console;

        [SetUp]
        public void SetUp()
        {
            _parser = Substitute.For<CommandParser>();
            _console = Substitute.For<IConsole>();
            _socialCmdApi = new SocialCmdApi(_parser,new CommandHandler());
        }

        [Test, Ignore("Add later")]
        public void should_print_the_users_timeline()
        {
            GivenUserPostedMessages();
            var instruction = $"{UserName}";

            _socialCmdApi.Handle(instruction);

            Received.InOrder(() =>
                    {
                        _console.Received().PrintLine("Good game though. (1 minute ago)");
                        _console.Received().PrintLine("Damn! We lost! (2 minutes ago)");
                    }
                );
        }

        private void GivenUserPostedMessages()
        {
            var postMessageCommand = $"{UserName} -> Good game though." ;
            var postAnotherMessageCommand = $"{UserName} -> Damn! We lost!";

            _socialCmdApi.Handle(postMessageCommand);
            _socialCmdApi.Handle(postAnotherMessageCommand);

        }
    }
}