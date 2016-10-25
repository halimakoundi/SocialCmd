using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd.Domain.Model
{
	public class User
	{
		public User (string userName)
		{
			UserName = userName;
			Posts = new List<Post> ();
			Followings = new List<User> ();
		}
		public string UserName { get;}
		public List<Post> Posts { get;}
		public List<User> Followings { get;}

	    public void Post(Post post){
			this.Posts.Add(post);
		}

	    public void Post(string message){
			this.Post(new Post(this.UserName, message, DateTime.Now));
		}

	    public void Follow(User user){
			if (!(Followings.Any(x => x.UserName == user.UserName))) {
				Followings.Add (user);
			}
		}

	    public override string ToString ()
		{
			return UserName != null && string.IsNullOrEmpty(UserName) ? UserName : "Unknown";
		}

	}
}

