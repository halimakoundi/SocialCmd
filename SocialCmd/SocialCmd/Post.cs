using System;

namespace SocialCmd
{
	public class Post
	{
		public Post (string userName, string message)
		{
			this.UserName = userName;
			this.Message = message;
			this.DatePosted = DateTime.Now;
		}
		public string UserName{ get;}
		public string Message{ get;}
		public DateTime DatePosted{ get;}

		/// <summary>
		/// Timelines the post date.
		/// </summary>
		/// <returns>The relative time of the post</returns>
		private string TimelinePostDate(){
			
			TimeSpan span = (DateTime.Now).Subtract ( DatePosted );
			var timeDifference = string.Empty;
			//do we want detailed timeline info i.e (1 minute, 23 seconds ago) or indicating the greater unit is enough ?
			if (span.Days > 0) {
				timeDifference += string.Format(" {0} day{1}", span.Days, (span.Days > 1 ? "s" : ""));
			}else if (span.Hours > 0) {
				timeDifference += string.Format(" {0} hour{1}", span.Hours, (span.Hours > 1 ? "s" : ""));
			}else if (span.Minutes > 0) {
				timeDifference += string.Format(" {0} minute{1}", span.Minutes, (span.Minutes > 1 ? "s" : ""));
			}else if (span.Seconds > 0) {
				timeDifference += string.Format(" {0} second{1}", span.Seconds, (span.Seconds > 1 ? "s" : ""));;
			}
			if(!string.IsNullOrEmpty(timeDifference)){
				timeDifference += " ago";
			}else { 
				timeDifference = " just now ";
			}
			return timeDifference ;
		}

		/// <summary>
		/// Writes the posted message to a wall.
		/// </summary>
		/// <returns>The formatted post message containing the user name , the message 
		/// and the relative time it has been posted</returns>
		public string WriteToWall(){
			return string.Format ("{0} - {1}", this.UserName, this.ToString ());
		}

		public override string ToString ()
		{
			return string.Format ("{0} ({1}){2}", Message, TimelinePostDate(), Environment.NewLine);
		}
	}
}

