using System;
using NSubstitute;
using NUnit.Framework;
using SocialCmd;
using SocialCmd.Domain.Model;

namespace SocialCmdTest
{
    [TestFixture]
    public class PrinterShould
    {
        private const string UserName = "John Doe";
        private DateProvider _dateProvider;
        private IConsole _console;


        [SetUp]
        public void SetUp()
        {
            _dateProvider = new DateProvider();
        }


        [Test, Ignore("Too soon")]
        public void print_post_indicating_it_has_just_been_posted()
        {
            var datePosted = DateTime.Now;
            var post = new Post(UserName, "new Post test", datePosted);
            var message = $"{post.Message} (just now)";

            var  printer = new Printer();
            //printer.PrintToTimeLine(post);

            _console.Received().Write(message);
        }

        [Test]
        public void TimelinePostDateTest()
        {
            var newPost = new Post("Test User", "new Post test", DateTime.Now);

            Assert.AreEqual("new Post test (just now)" + Environment.NewLine, Printer.PrintToTimeLine(newPost));

            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual("new Post test (1 second ago)" + Environment.NewLine, Printer.PrintToTimeLine(newPost));

            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual("new Post test (2 seconds ago)" + Environment.NewLine, Printer.PrintToTimeLine(newPost));
            /*
			System.Threading.Thread.Sleep(60000);
			Assert.AreEqual ("new Post test (1 minute ago)" + Environment.NewLine, newPost.ToString());

			System.Threading.Thread.Sleep(60000);
			Assert.AreEqual ("new Post test (2 minutes ago)" + Environment.NewLine, newPost.ToString());
*/
        }

    }
}

