using NSubstitute;
using NUnit.Framework;
using SocialCmd;

namespace SocialCmdTest
{
    [TestFixture]
    public class PublishUserTimelineFeature
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
            _socialCmdApi = new SocialCmdApi(_parser, _console);
        }

        [Test]
        public void should_print_the_users_timeline()
        {
            GivenUserPostedMessages();
            var printTimelineCommand = $"{UserName}";

            _socialCmdApi.Execute(printTimelineCommand);

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

            _socialCmdApi.Execute(postMessageCommand);
            _socialCmdApi.Execute(postAnotherMessageCommand);

        }
    }
}