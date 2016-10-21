using System;
using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using SocialCmd;

namespace SocialCmdTest
{
    [TestFixture()]
    public class SocialCmdApiShould
    {
        private Dictionary<String, CmdKey> _cmdKeys = new Dictionary<String, CmdKey>();

        [SetUp]
        public void InitialiseTestFixture()
        {
            _cmdKeys = Settings.ValidCommands();
        }

        [Test]
        public void post_user_message()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> This is a test message");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void not_post_user_message_given_equals_key_isnot_defined()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser = This is a test message");

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void post_user_message_given_equals_key_is_defined()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> I am calling this only to create the user");
            _cmdKeys.Add("=", CmdKey.Post);

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser = This is a test message");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void not_read_messages_for_user_that_does_not_exist()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser ");

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void read_messages_for_user()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser -> This is a test message");
            Assert.IsTrue(result.Success);

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public  void read_user_messages()
        {
            _cmdKeys.Add("read", CmdKey.Read);
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> This is a test message");

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser read");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void allow_when_an_existing_user_follows_another_user()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUser -> This is a test message");
            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "anotherUser -> This is another test message");

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUser follows anotherUser ");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void not_add_follower_to_user_that_do_not_exist()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUserThatDoNotExist follows anotherUser ");

            Assert.IsFalse(result.Success);
        }

        [Test()]
        public void ExecuteCommandAndReturnResult_WallTest()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser -> I am calling this to create the user");

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser wall");

            Assert.IsTrue(result.Success);

        }

        [Test]
        public void not_print_wall_given_user_does_not_exist()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUserDoesNotExist wall");
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void print_wall_when_mur_key_is_defined_and_used()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser -> This is a test message");
            _cmdKeys.Add("mur", CmdKey.PrintWall);

            result = SocialCmdApi.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser mur");

            Assert.IsTrue(result.Success);
        }
    }
}

