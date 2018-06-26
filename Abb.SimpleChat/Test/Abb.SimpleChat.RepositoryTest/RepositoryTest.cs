using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abb.SimpleChat.Business.Logic.Entities;
using Abb.SimpleChat.External.RepositoryEntityFamework;
using Moq;
using System.IO;
using System;

namespace Abb.SimpleChat.RepositoryTest
{
    [TestClass]
    public class RepositoryTest
    {
        private const string DatabasePath =
            "C:\\Users\\Сергей\\source\\repos\\Abb.SimpleChat\\Test\\Abb.SimpleChat.RepositoryTest";
        private const string DatabaseName = "TestBase.db3";
        private Users user;
        private Roles role;
        private IDatabaseSettings databaseSettings;
        private Messages message;
        SimpleChatRepository<Users> userRepository;
        SimpleChatRepository<Roles> roleRepository;
        SimpleChatRepository<Messages> messageRepository;

        [TestInitialize]
        public void Init()
        {
            if (File.Exists($"{DatabasePath}\\{DatabaseName}"))
                File.Delete($"{DatabasePath}\\{DatabaseName}");
            databaseSettings = Mock.Of<IDatabaseSettings>();
            databaseSettings.ConnnectionString = $"{DatabasePath}\\{DatabaseName}";
        }

        [TestMethod]
        public void AddGetUpdateDeleteUser()
        {
            userRepository = new SimpleChatRepository<Users>(databaseSettings);
            userRepository.CreatDatabase();
            user = new Users
            {
                //Id = 1,
                Name = "Serge"
            };

            userRepository.Add(user);
            userRepository.Save();
            var userFromDb = userRepository.GetItem(1);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(userFromDb.Id, 1);
            Assert.AreEqual(userFromDb.Name, "Serge");

            user.Name = "Andrei";
            userRepository.Update(user);
            userRepository.Save();
            userFromDb = userRepository.GetItem(1);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(userFromDb.Id, 1);
            Assert.AreEqual(userFromDb.Name, "Andrei");

            userRepository.DeleteItem(1);
            userRepository.Save();
            userFromDb = userRepository.GetItem(1); ;
            Assert.IsNull(userFromDb);

            userRepository.ContextDispose();
        }

        [TestMethod]
        public void AddGetUpdateDeleteRole()
        {
            roleRepository = new SimpleChatRepository<Roles>(databaseSettings);

            role = new Roles
            {
                NameRole = "main",
                Id = 1
            };

            roleRepository.Add(role);
            roleRepository.Save();
            var roleFromDb = roleRepository.GetItem(1);

            Assert.IsNotNull(role);
            Assert.AreEqual(roleFromDb.Id, 1);
            Assert.AreEqual(roleFromDb.NameRole, "main");

            role.NameRole = "admin";
            roleRepository.Update(role);
            roleRepository.Save();
            roleFromDb = roleRepository.GetItem(1);

            Assert.IsNotNull(roleFromDb);
            Assert.AreEqual(roleFromDb.Id, 1);
            Assert.AreEqual(roleFromDb.NameRole, "admin");

            roleRepository.DeleteItem(1);
            roleRepository.Save();
            roleFromDb = roleRepository.GetItem(1);
            Assert.IsNull(roleFromDb);

            roleRepository.ContextDispose();
        }

        [TestMethod]
        public void AddGetUpdateDeleteMessage()
        {
            messageRepository = new SimpleChatRepository<Messages>(databaseSettings);

            message = new Messages
            {
                Id = 1,
                UserId = 1,
                Text = "textofmessage"
            };

            messageRepository.Add(message);
            messageRepository.Save();
            var messageFromDb = messageRepository.GetItem(1);

            Assert.IsNotNull(message);
            Assert.AreEqual(messageFromDb.Id, 1);
            Assert.AreEqual(messageFromDb.UserId, 1);
            Assert.AreEqual(messageFromDb.Text, "textofmessage");

            message.Text = "anotherText";
            messageRepository.Update(message);
            messageRepository.Save();
            messageFromDb = messageRepository.GetItem(1);

            Assert.IsNotNull(messageFromDb);
            Assert.AreEqual(messageFromDb.Id, 1);
            Assert.AreEqual(messageFromDb.UserId, 1);
            Assert.AreEqual(messageFromDb.Text, "anotherText");

            messageRepository.DeleteItem(1);
            messageRepository.Save();
            messageFromDb = messageRepository.GetItem(1);
            Assert.IsNull(messageFromDb);

            messageRepository.ContextDispose();
        }

        [TestCleanup]
        public void End()
        {
            if (File.Exists($"{DatabasePath}\\{DatabaseName}"))
                File.Delete($"{DatabasePath}\\{DatabaseName}");
        }

        [ExpectedException(typeof(ArgumentException))]
        public void CtorNulSettingsExceptionThro()
        {
            userRepository = new SimpleChatRepository<Users>(null);
        }
    }
}
