using System;

namespace SocialCmd.Domain.Model
{
    public static class Printer
    {
        public static string PrintToTimeLine(Post post, DateProvider dateProvider)
        {
            return $"{post.Message} ({dateProvider.TimelinePostDate(post.DatePosted)}){Environment.NewLine}";
        }

        public static string PrintToWall(Post post)
        {
            return $"{post.UserName} - {post}";
        }
    }
}