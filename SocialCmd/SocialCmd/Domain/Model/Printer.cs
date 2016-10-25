using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd.Domain.Model
{
    public class Printer
    {
        private static readonly DateProvider DateProvider= new DateProvider();

        public static string PrintToTimeLine(Post post)
        {
            return $"{post.Message} ({DateProvider.TimelinePostDate(post.DatePosted)}){Environment.NewLine}";
        }

        public static string PrintToWall(Post post)
        {
            return $"{post.UserName} - {PrintToTimeLine(post)}";
        }

        public static string WriteToWall(User user){
            var messages = String.Empty;
            var postsList = new List<Post>();
            postsList.AddRange (user.Followings.SelectMany (x => x.Posts).ToList());
            postsList.AddRange (user.Posts);
            postsList = postsList.OrderByDescending (x => x.DatePosted).ToList();

            foreach(var post in postsList){
                messages += PrintToWall(post);
            }

            return messages;
        }

        public static string Read(User user){
            var postsStr = String.Empty;
            var posts = user.Posts.OrderByDescending (x => x.DatePosted).ToList();
            foreach (Post post in posts) {
                postsStr += Printer.PrintToTimeLine(post) ;
            }
            return postsStr;
        }
    }
}