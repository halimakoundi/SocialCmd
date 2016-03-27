using System;
using NUnit.Framework;
using SocialCmd;

namespace SocialCmdTest
{
	[TestFixture ()]
	public class PostTests
	{
		[Test ()]
		public void TimelinePostDateTest()
		{
			var newPost = new Post ("Test User", "new Post test");

			Assert.AreEqual ("new Post test (just now)" + Environment.NewLine, newPost.ToString());

			System.Threading.Thread.Sleep(1000);
			Assert.AreEqual ("new Post test (1 second ago)" + Environment.NewLine, newPost.ToString());

			System.Threading.Thread.Sleep(1000);
			Assert.AreEqual ("new Post test (2 seconds ago)" + Environment.NewLine, newPost.ToString());
			/*
			System.Threading.Thread.Sleep(60000);
			Assert.AreEqual ("new Post test (1 minute ago)" + Environment.NewLine, newPost.ToString());

			System.Threading.Thread.Sleep(60000);
			Assert.AreEqual ("new Post test (2 minutes ago)" + Environment.NewLine, newPost.ToString());
*/
		}



	}
}

