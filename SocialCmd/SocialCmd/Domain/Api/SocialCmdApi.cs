﻿using System;
using System.Collections.Generic;

namespace SocialCmd
{
	public class SocialCmdApi
	{
		private static Dictionary<String, User> appUsers = new Dictionary<String, User> ();

		public static QualifiedBoolean ExecuteCommandAndReturnResult(Dictionary<String,CmdKey> cmdKeys, string enteredCommand){
			QualifiedBoolean result = new QualifiedBoolean();
			//getting the number of words in the entered command considering the user name cannot contain spaces				
			var commandParts = enteredCommand.Trim().Split(' ');
			var userName = commandParts [0].ToLower ();
			CmdKey currentKey;

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
			}else {					
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

		public static QualifiedBoolean PostMessageToUser(String userName, String message){
			QualifiedBoolean result = new QualifiedBoolean();
			User user;
			UserExists (userName, out  user, out result, true);
			if(user != null) user.Post (message);
			return result;
		}

		public static QualifiedBoolean ReadUserPosts(String userName){
			QualifiedBoolean result = new QualifiedBoolean();
			User user;
			UserExists (userName, out  user, out result);
			if (user != null)
				result.Value = user.Read ();
			return result;
		}

		public static QualifiedBoolean PrintUserWall(String userName){
			QualifiedBoolean result = new QualifiedBoolean();
			User user;
			var userExist = UserExists (userName, out  user, out result);
			if (userExist && user != null)
				result.Value = user.WriteToWall ();
			return result;
		}

		public static QualifiedBoolean UserFollowAnotherUser(String userName, String userNameToFollow){
			QualifiedBoolean result = new QualifiedBoolean();
			User user;
			var userExist = UserExists (userName, out user, out result);

			User userToFollow;
			var usertoFollowExist = UserExists (userNameToFollow, out userToFollow, out result);

			if (userExist && usertoFollowExist && user != null && userToFollow != null) {
				user.Follow (userToFollow);
			} else {
				result.Value = "Can not follow user, a problem occured.";
				result.Success = false;
			}
			return result;
		}

		public static Boolean UserExists(String userName, out User user, out QualifiedBoolean result, Boolean createIfNotExist=false){
			result = new QualifiedBoolean();
			var userExist = appUsers.TryGetValue (userName, out user);	
			if (!userExist && createIfNotExist) {
				user = new User(userName.ToLower ());
				appUsers.Add (user.UserName, user);
			} else if (!userExist){
				result.Value = "User does not exist.";
				result.Success = false;	
			}
			return userExist;
		}
	}
}

