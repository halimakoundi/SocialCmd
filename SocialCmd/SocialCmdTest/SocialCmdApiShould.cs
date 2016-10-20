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
        static Dictionary<String, CmdKey> cmdKeys = new Dictionary<String, CmdKey>();
        private CommandParser _parser;
        private ICommand _command;
        private IConsole _console;
        private SocialCmdApi _socialCmdApp;

        [SetUp]
        public void InitialiseTestFixture()
        {
            cmdKeys = Settings.ValidCommands();
            _parser = Substitute.For<CommandParser>();
            _console = Substitute.For<IConsole>();
            _socialCmdApp = new SocialCmdApi(_parser, _console);

            _command = Substitute.For<ICommand>();
        }

        [Test]
        public void execute_post_command()
        {
            var commandAsAString = "Bob -> Damn! We lost!";
            _parser.Parse(commandAsAString).Returns(_command);

            _socialCmdApp.Execute(commandAsAString);

            Received.InOrder(() =>
            {
                _parser.Received().Parse(commandAsAString);
                _command.Received().Execute();
            });
        }


        [Test()]
        public void ExecuteCommandAndReturnResult_PostTest()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "TestUser = This is a test message");
            Assert.IsFalse(result.Success);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "TestUser -> This is a test message");
            Assert.IsTrue(result.Success);
            cmdKeys.Add("=", CmdKey.Post);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "TestUser = This is a test message");
            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void ExecuteCommandAndReturnResult_ReadTest()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "ReadTestUser ");
            Assert.IsFalse(result.Success);
            //post a message to create test user
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "ReadTestUser -> This is a test message");
            Assert.IsTrue(result.Success);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "ReadTestUser");
            Assert.IsTrue(result.Success);
            cmdKeys.Add("read", CmdKey.Read);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "TestUser read");
            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void ExecuteCommandAndReturnResult_FollowTest()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "FollowTestUser follows anotherUser ");
            Assert.IsFalse(result.Success);
            //post messages to create test users
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "FollowTestUser -> This is a test message");
            Assert.IsTrue(result.Success);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "anotherUser -> This is another test message");
            Assert.IsTrue(result.Success);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "FollowTestUser follows anotherUser ");
            Assert.IsTrue(result.Success);
        }

        [Test()]
        public void ExecuteCommandAndReturnResult_WallTest()
        {
            var result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "WallTestUser wall");
            Assert.IsFalse(result.Success);
            //post messages to create test users
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "WallTestUser -> This is a test message");
            Assert.IsTrue(result.Success);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "WallTestUser wall");
            Assert.IsTrue(result.Success);
            cmdKeys.Add("mur", CmdKey.PrintWall);
            result = SocialCmdApi.ExecuteCommandAndReturnResult(cmdKeys, "WallTestUser mur");
            Assert.IsTrue(result.Success);
        }
    }
}

