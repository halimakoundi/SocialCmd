using System;

namespace SocialCmd.Domain.Model
{
    public static class Printer
    {
        public static string PrintToTimeLine(Post post, DateProvider dateProvider)
        {
            return $"{post.Message} ({dateProvider.TimelinePostDate(post.DatePosted)}){Environment.NewLine}";
        }
    }
}