using System;
using System.Collections.Generic;

namespace SocialCmd
{
	public class Settings
	{ 
		private Dictionary<String,CmdKey> Commands { get; set;}
		private static Settings _Instance;

		private Settings ()
		{
			InitWithDefaultSettings ();
		}

		private Settings (Dictionary<String,CmdKey> commands)
		{
			this.Commands = commands;
		}

		public static Dictionary<String, CmdKey> ValidCommands(){
			if (_Instance == null) {
				_Instance = new Settings ();
			}
			return _Instance.Commands;
		}

		public static Dictionary<String, CmdKey> ValidCommands(Dictionary<String,CmdKey> commands){
			if (_Instance == null) {
				_Instance = new Settings (commands);
			}
			return _Instance.Commands;
		}

		private void InitWithDefaultSettings(){
			this.Commands = new Dictionary<String,CmdKey>();
			this.Commands.Add("->", CmdKey.Post);
			this.Commands.Add("follows", CmdKey.Follow);
			this.Commands.Add("wall", CmdKey.PrintWall);
		}
	}
}

