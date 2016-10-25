using NUnit.Framework;
using System;
using SocialCmd.Domain.Model;

namespace SocialCmdTest
{
	[TestFixture ()]
	public class UserTests
	{
	    private DateProvider _dateProvider;

        [SetUp]
	    public void SetUp()
	    {
	        _dateProvider = new DateProvider();
	    }

	    [Test ()]
		public void PostingTest ()
		{
			var user = new User ("Alice");

			Assert.AreEqual ("Alice", user.UserName);
	        var newPost = new Post (user.UserName, "We lost!", DateTime.Now);
			user.Post(newPost );
			Assert.IsTrue (user.Posts.Count > 0);
			Assert.AreEqual (Printer.PrintToTimeLine(newPost),Printer.PrintToTimeLine(user.Posts[0]));
		}

	    [Test ()]
		public void ReadingTest ()
		{
			var user = new User ("Alice");
			Assert.AreEqual ("", user.Read ());
			var newPost = new Post(user.UserName, "This a great day :-) ", DateTime.Now);
			user.Post (newPost);
			Assert.AreEqual (Printer.PrintToTimeLine(newPost), user.Read ());
		}

		[Test ()]
		public void FollowTest ()
		{
			var user = new User ("Alice");
			var userBob = new User ("Bob");
			user.Follow (userBob);
			Assert.IsTrue (user.Followings.Count > 0);
			user.Follow (userBob);
			Assert.IsFalse (user.Followings.Count > 1);
			Assert.AreEqual (userBob, user.Followings [0]);
		}

		[Test ()]
		public void WallTest ()
		{
			var user = new User ("Alice");
			var userBob = new User ("Bob");
			user.Follow (userBob);

			var bobPost = new Post (userBob.UserName, "Thank God it's Friday !", DateTime.Now);
			userBob.Post (bobPost);

			//wait 5 seconds before posting new message
			System.Threading.Thread.Sleep(5000);
			var newPost = new Post(user.UserName, "This is a great day :-) ", DateTime.Now);
			user.Post (newPost);

            Assert.AreEqual ($"{Printer.PrintToWall(newPost)}{Printer.PrintToWall(bobPost)}", 
								Printer.WriteToWall(user));
		}
	}
}

