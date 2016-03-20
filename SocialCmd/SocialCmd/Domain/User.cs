using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd
{
	public class User
	{
		public User (string userName)
		{
			this.UserName = userName;
			this.Posts = new List<Post> ();
			this.Followings = new List<User> ();
		}
		public string UserName { get;}
		public List<Post> Posts { get;}
		public List<User> Followings { get;}

		public string Read(){
			var postsStr = string.Empty;
			var posts = this.Posts.OrderByDescending (x => x.DatePosted).ToList();
			foreach (Post post in posts) {
				postsStr += post.ToString () ;
			}
			return postsStr;
		}

		public void Post(Post post){
			this.Posts.Add(post);
		}

		public void Post(string message){
			this.Post(new Post(this.UserName, message));
		}

		public void Follow(User user){
			if (!(this.Followings.Where (x => x.UserName == user.UserName).Count() > 0)) {
				this.Followings.Add (user);
			}
		}

		/// <summary>
		/// Writes to wall.
		/// </summary>
		/// <returns>Writes to the wall,the list of posts of the user, and the followed users ordered by post date.</returns>
		public string WriteToWall(){
			var messages = string.Empty;
			var postsList = new List<Post> ();
			postsList.AddRange (this.Followings.SelectMany (x => x.Posts).ToList());
			postsList.AddRange (this.Posts);
			postsList = postsList.OrderByDescending (x => x.DatePosted).ToList();

			foreach(var post in postsList){
				messages += post.WriteToWall();
			}

			return messages;
		}

		public override string ToString ()
		{
			return UserName != null && string.IsNullOrEmpty(UserName) ? UserName : "Unknown";
		}

	}
}

