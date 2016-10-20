using System;
using System.Collections.Generic;

namespace SocialCmd
{
    public class Timeline
    {
        private readonly User _user;
        private readonly Posts _posts = new Posts();

        private Timeline(User user)
        {
            _user = user;
        }

        public static Timeline For(User user)
        {
            return new Timeline(user);
        }

        public void Post(string message)
        {
            _posts.Add(new Post(message));
        }
    }

    internal class Posts
    {
        private readonly List<Post> _posts = new List<Post>();

        public void Add(Post post)
        {
            _posts.Add(post);
        }
    }
}