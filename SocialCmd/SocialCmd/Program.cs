using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialCmd
{
	class MainClass
	{
		static Dictionary<String, User> appUsers = new Dictionary<String, User> ();
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

			QualifiedBoolean result = new QualifiedBoolean();
			User currentUser;
			CmdKey currentKey;

			//getting the number of words in the entered command considering the user name cannot contain spaces				
			var commandParts = enteredCommand.Trim().Split(' ');
			var userName = commandParts [0].ToLower ();

			var userExist = appUsers.TryGetValue (userName, out currentUser);

			if (commandParts.Length > 2) {	
				var key = commandParts [1].ToLower ();
				var keyExist = cmdKeys.TryGetValue (key, out currentKey);

				if (keyExist) {						
					if (currentKey == CmdKey.Post) {
						var message = enteredCommand.Split (new string[]{ key }, StringSplitOptions.RemoveEmptyEntries) [1];
						result = SocialCmdApi.PostMessageToUser (userName,message );
					}else if (currentKey == CmdKey.Follow) {	
						var userNameToFollow = commandParts[2].ToLower ();
						result = SocialCmdApi.UserFollowAnotherUser (userName, userNameToFollow );
					}
				} else {
					//ask for another command						
					result.Value = "Command not recognised.";
					result.Success = false;
				}		
			} else {					
				switch (commandParts.Length) {		
				case 1:						
					//Here only the username has been typed - read posts for that user
					result = SocialCmdApi.ReadUserPosts (userName);
					break;						
				case 2:		
					var key = commandParts [1].ToLower ();
					var keyExist = cmdKeys.TryGetValue (key, out currentKey);						
					if (keyExist && currentKey == CmdKey.PrintWall) {								
						result = SocialCmdApi.PrintUserWall (userName);						
					}else {
						//ask for another command						
						result.Value = "Command not recognised.";
						result.Success = false;
					}							
					break;						
				}					
			}
			return result;
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
