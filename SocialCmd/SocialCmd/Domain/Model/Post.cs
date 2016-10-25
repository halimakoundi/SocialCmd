using System;

namespace SocialCmd.Domain.Model
{
	public class Post
	{
        private string v;

        public Post (string userName, string message)
		{
			UserName = userName;
            Message = message;
            DatePosted = DateTime.Now;
		}

        public string UserName{ get;}
		public string Message{ get;}
		public DateTime DatePosted{ get;}

		private string TimelinePostDate(){
			
			var span = (DateTime.Now).Subtract ( DatePosted );
			var timeDifference = string.Empty;
			if (span.Days > 0) {
				timeDifference += $"{span.Days} day{(span.Days > 1 ? "s" : "")}";
			}else if (span.Hours > 0) {
				timeDifference += $"{span.Hours} hour{(span.Hours > 1 ? "s" : "")}";
			}else if (span.Minutes > 0) {
				timeDifference += $"{span.Minutes} minute{(span.Minutes > 1 ? "s" : "")}";
			}else if (span.Seconds > 0) {
				timeDifference += $"{span.Seconds} second{(span.Seconds > 1 ? "s" : "")}";;
			}
			if(!string.IsNullOrEmpty(timeDifference)){
				timeDifference += " ago";
			}else { 
				timeDifference = "just now";
			}
			return timeDifference ;
		}

		public string WriteToWall(){
			return $"{UserName} - {this}";
		}

		public override string ToString ()
		{
			return $"{Message} ({TimelinePostDate()}){Environment.NewLine}";
		}
	}
}

