using System;

namespace SocialCmd.Domain.Model
{
	public class Post
	{
	    public Post (string userName, string message, DateTime datePosted)
		{
			UserName = userName;
            Message = message;
	        DatePosted = datePosted;
		}

        public string UserName{ get;}
		public string Message{ get;}
		public DateTime DatePosted{ get;}
	}
}

