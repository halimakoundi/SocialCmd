using System;

namespace SocialCmd.Domain.Model
{
	public class Post
	{
	    private readonly DateProvider _dateProvider;

	    public Post (string userName, string message, DateProvider dateProvider)
		{
			UserName = userName;
            Message = message;
	        _dateProvider = dateProvider;
	        DatePosted = DateTime.Now;
		}

        public string UserName{ get;}
		public string Message{ get;}
		public DateTime DatePosted{ get;}

	    public string WriteToWall(){
			return Printer.PrintToWall(this);
		}

	    public override string ToString ()
		{
			return Printer.PrintToTimeLine(this, _dateProvider);
		}
	}
}

