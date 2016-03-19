using NUnit.Framework;
using System;
using SocialCmd;

namespace SocialCmdTest
{
	[TestFixture ()]
	public class UserTest
	{
		[Test ()]
		public void PostingTest ()
		{
			var user = new User ("Alice");

			Assert.AreEqual ("Alice", user.UserName);
			var newPost = new Post (user.UserName, "We lost!");
			user.Post(newPost );
			Assert.IsTrue (user.Posts.Count > 0);
			Assert.AreEqual (newPost.ToString(),user.Posts[0].ToString());
		}

		[Test ()]
		public void ReadingTest ()
		{
			var user = new User ("Alice");
			Assert.AreEqual ("", user.Read ());
			var newPost = new Post(user.UserName, "This a great day :-) ");
			user.Post (newPost);
			Assert.AreEqual (newPost.ToString (), user.Read ());
		}

		[Test ()]
		public void FollowTest ()
		{
			var user = new User ("Alice");
			var userBob = new User ("Bob");
			user.Follow (userBob);
			Assert.IsTrue (user.Followings.Count > 0);
			Assert.AreEqual (userBob, user.Followings [0]);
		}

		[Test ()]
		public void WallTest ()
		{
			var user = new User ("Alice");
			var userBob = new User ("Bob");
			user.Follow (userBob);

			var bobPost = new Post (userBob.UserName, "Thank God it's Friday !");
			userBob.Post (bobPost);

			//wait 10 seconds before posting new message
			System.Threading.Thread.Sleep(10000);
			var newPost = new Post(user.UserName, "This is a great day :-) ");
			user.Post (newPost);

			Assert.AreEqual (string.Format("{0}{1}", newPost.WriteToWall(), bobPost.WriteToWall()), 
								user.WriteToWall());
		}
	}
}

