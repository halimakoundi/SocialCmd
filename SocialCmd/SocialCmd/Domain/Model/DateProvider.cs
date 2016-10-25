using System;

namespace SocialCmd.Domain.Model
{
    public class DateProvider
    {
        public string TimelinePostDate(DateTime datePosted){
			
            var span = (DateTime.Now).Subtract ( datePosted );
            var timeDifference = String.Empty;
            if (span.Days > 0) {
                timeDifference += $"{span.Days} day{(span.Days > 1 ? "s" : "")}";
            }else if (span.Hours > 0) {
                timeDifference += $"{span.Hours} hour{(span.Hours > 1 ? "s" : "")}";
            }else if (span.Minutes > 0) {
                timeDifference += $"{span.Minutes} minute{(span.Minutes > 1 ? "s" : "")}";
            }else if (span.Seconds > 0) {
                timeDifference += $"{span.Seconds} second{(span.Seconds > 1 ? "s" : "")}";;
            }
            if(!String.IsNullOrEmpty(timeDifference)){
                timeDifference += " ago";
            }else { 
                timeDifference = "just now";
            }
            return timeDifference ;
        }
    }
}