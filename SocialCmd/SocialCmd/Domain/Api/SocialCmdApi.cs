using System;
using System.Collections.Generic;

namespace SocialCmd
{
	public class SocialCmdApi
	{
		private static Dictionary<String, User> appUsers = new Dictionary<String, User> ();

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

		private static Boolean UserExists(String userName, out User user, out QualifiedBoolean result, Boolean createIfNotExist=false){
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

