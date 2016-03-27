using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd
{
	class MainClass
	{
		static Dictionary<String,CmdKey> cmdKeys = new Dictionary<String,CmdKey>();

		public static void Main (string[] args)
		{			
			InitialiseApplication ();
			do {
				var enteredCommand = PromptUserForCommand ();
				var result = ExecuteCommandAndReturnResult (enteredCommand);
				if (result.Success) {
					Console.WriteLine(result.Value);
				} else {
					ApplicationError (result.Value);
				}
			} while(true);				
		}

		private static QualifiedBoolean ExecuteCommandAndReturnResult(string enteredCommand){
			
			return SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, enteredCommand);
		}

		private static string PromptUserForCommand(){
			Console.Write ("> ");				
			return Console.ReadLine().Trim();
		}

		private static void InitialiseApplication(){
			cmdKeys = Settings.ValidCommands ();
		}

		private static void ApplicationError(String message){
			Console.WriteLine ("A problem occured : " + message);
		}

	}
}
