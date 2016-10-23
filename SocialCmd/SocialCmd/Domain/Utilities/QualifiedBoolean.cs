using System;
using static System.String;

namespace SocialCmd
{
	public class QualifiedBoolean
	{
		public bool Success { get; set;}=true;
		public string Value { get; set;}=Empty;

		public QualifiedBoolean (bool result)
		{			
			this.Success = result;
		}
		public QualifiedBoolean (bool result, string value)
		{			
			this.Success = result;
			this.Value = value;
		}
		public QualifiedBoolean ()
		{			
		}
	}
}

