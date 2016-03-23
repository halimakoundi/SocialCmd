using System;

namespace SocialCmd
{
	public class QualifiedBoolean
	{
		public Boolean Success { get; set;}=true;
		public String Value { get; set;}=String.Empty;

		public QualifiedBoolean (Boolean result)
		{			
			this.Success = result;
		}
		public QualifiedBoolean (Boolean result, string value)
		{			
			this.Success = result;
			this.Value = value;
		}
		public QualifiedBoolean ()
		{			
		}
	}
}

