using System;

namespace SocialCmd.Domain.Model
{
    public static class Printer
    {
        private static readonly DateProvider DateProvider= new DateProvider();

        public static string PrintToTimeLine(Post post)
        {
            return $"{post.Message} ({DateProvider.TimelinePostDate(post.DatePosted)}){Environment.NewLine}";
        }

        public static string PrintToWall(Post post)
        {
            return $"{post.UserName} - {post}";
        }
    }
}