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
						if (!userExist) {
							currentUser = new User(userName);
							appUsers.Add (currentUser.UserName, currentUser);
						}
						currentUser.Post (enteredCommand.Split (new string[]{ key }, StringSplitOptions.RemoveEmptyEntries) [1]);

					}else if (currentKey == CmdKey.Follow) {							
						User userToFollow;
						var userNameToFollow = commandParts[2].ToLower ();
						var userToFollowExists = appUsers.TryGetValue (userNameToFollow, out userToFollow);							
						if (userToFollowExists && userToFollow != null) {								
							currentUser.Follow (userToFollow);
						} else {				
							result.Value = "User to follow does not exist.";
							result.Success = false;
						}
					}
				} else {
					//ask for another command						
					result.Value = "Command not recognised.";
					result.Success = false;
				}		
			} else if (userExist) {					
				switch (commandParts.Length) {		
				case 1:						
					//Here only the username has been typed - read posts for that user						
					result.Value = currentUser.Read ();							
					break;						
				case 2:		
					var key = commandParts [1].ToLower ();
					var keyExist = cmdKeys.TryGetValue (key, out currentKey);						
					if (keyExist && currentKey == CmdKey.PrintWall) {								
						result.Value  = currentUser.WriteToWall ();							
					}else {
						//ask for another command						
						result.Value = "Command not recognised.";
						result.Success = false;
					}							
					break;						
				}					
			}else {					
				//ask for another command					
				result.Value = enteredCommand.Trim().Length > 0 ? "User does not exist" : "Command cannot be empty";
				result.Success = false;
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
