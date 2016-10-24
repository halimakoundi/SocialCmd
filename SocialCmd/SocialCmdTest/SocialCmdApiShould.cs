using System;
using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using SocialCmd;
using SocialCmd.Domain.Api;

namespace SocialCmdTest
{
    [TestFixture()]
    public class SocialCmdApiShould
    {
        private Dictionary<String, CmdKey> _cmdKeys = new Dictionary<String, CmdKey>();
        private UserRepository _userRepository;
        private SocialCmdApp _socialCmdApp;

        [SetUp]
        public void InitialiseTestFixture()
        {
            _cmdKeys = Settings.ValidCommands();
            _userRepository = Substitute.For<UserRepository>();
            _socialCmdApp = new SocialCmdApp(_userRepository);
        }

        [Test]
        public void post_user_message()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> This is a test message");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void not_post_user_message_given_equals_key_isnot_defined()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser = This is a test message");

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void post_user_message_given_equals_key_is_defined()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> I am calling this only to create the user");
            _cmdKeys.Add("=", CmdKey.Post);

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser = This is a test message");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void not_read_messages_for_user_that_does_not_exist()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser ");

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void read_messages_for_user()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser -> This is a test message");
            Assert.IsTrue(result.Success);

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "ReadTestUser");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public  void read_user_messages()
        {
            _cmdKeys.Add("read", CmdKey.Read);
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser -> This is a test message");

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "TestUser read");

            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void allow_when_an_existing_user_follows_another_user()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUser -> This is a test message");
            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "anotherUser -> This is another test message");

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUser follows anotherUser ");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void not_add_follower_to_user_that_do_not_exist()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "FollowTestUserThatDoNotExist follows anotherUser ");

            Assert.IsFalse(result.Success);
        }

        [Test()]
        public void ExecuteCommandAndReturnResult_WallTest()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser -> I am calling this to create the user");

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser wall");

            Assert.IsTrue(result.Success);

        }

        [Test]
        public void not_print_wall_given_user_does_not_exist()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUserDoesNotExist wall");
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void print_wall_when_mur_key_is_defined_and_used()
        {
            var result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser -> This is a test message");
            _cmdKeys.Add("mur", CmdKey.PrintWall);

            result = _socialCmdApp.ExecuteCommandAndReturnResult(_cmdKeys, "WallTestUser mur");

            Assert.IsTrue(result.Success);
        }
    }
}

