namespace Abb.SimpleChat.ContextTest
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Abb.SimpleChat.Business.Logic.Entities;
    using Abb.SimpleChat.External.RepositoryEntityFamework;
    using Moq;

    [TestClass]
    public class SimpleChatContextTest
    {
        private const string DatabasePath =
            "C:\\Users\\Сергей\\source\\repos\\Abb.SimpleChat\\Test\\Abb.SimpleChat.ContextTest";
        private const string DatabaseName = "TestBase.db3";
        private Users user;
        private Roles role;
        private Messages message;
        private IDatabaseSettings databaseSettings;
        SimpleChatContext context;
        
        [TestInitialize]
        public void Init()
        {
            if (File.Exists($"{DatabasePath}\\{DatabaseName}"))
                File.Delete($"{DatabasePath}\\{DatabaseName}");
            databaseSettings = Mock.Of<IDatabaseSettings>();
            databaseSettings.ConnnectionString = $"{DatabasePath}\\{DatabaseName}";
            context = new SimpleChatContext(databaseSettings);
            context.Database.EnsureCreated();
        }

        [TestMethod]
        public void AddGetUpdateDeleteUser()
        {
            user = new Users
            {
                Id = 1,
                Name = "Serge"
            };

            context.Users.Add(user);
            context.SaveChanges();
            var userFromDb = context.Users.Find(1);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(userFromDb.Id, 1);
            Assert.AreEqual(userFromDb.Name, "Serge");
            
            user.Name = "Andrei";
            context.Users.Update(user);
            context.SaveChanges();
            userFromDb = context.Users.Find(1);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(userFromDb.Id, 1);
            Assert.AreEqual(userFromDb.Name, "Andrei");

            context.Users.Remove(user);
            context.SaveChanges();
            userFromDb = context.Users.Find(1);
            Assert.IsNull(userFromDb);
        }

        [TestMethod]
        public void AddGetUpdateDeleteRole()
        {
            role = new Roles
            {
                NameRole="main",
                Id = 1
            };

            context.Roles.Add(role);
            context.SaveChanges();
            var roleFromDb = context.Roles.Find(1);

            Assert.IsNotNull(role);
            Assert.AreEqual(roleFromDb.Id, 1);
            Assert.AreEqual(roleFromDb.NameRole, "main");

            role.NameRole = "admin";
            context.Roles.Update(role);
            context.SaveChanges();
            roleFromDb = context.Roles.Find(1);

            Assert.IsNotNull(roleFromDb);
            Assert.AreEqual(roleFromDb.Id, 1);
            Assert.AreEqual(roleFromDb.NameRole, "admin");

            context.Roles.Remove(role);
            context.SaveChanges();
            roleFromDb = context.Roles.Find(1);
            Assert.IsNull(roleFromDb);
        }

        [TestMethod]
        public void AddGetUpdateDeleteMessage()
        {
            message = new Messages
            {
                Id = 1,
                UserId = 1,
                Text = "textofmessage"
            };

            context.Messages.Add(message);
            context.SaveChanges();
            var messageFromDb=context.Messages.Find(1);

            Assert.IsNotNull(message);
            Assert.AreEqual(messageFromDb.Id, 1);
            Assert.AreEqual(messageFromDb.UserId, 1);
            Assert.AreEqual(messageFromDb.Text, "textofmessage");

            message.Text = "anotherText";
            context.Messages.Update(message);
            context.SaveChanges();
            messageFromDb = context.Messages.Find(1);

            Assert.IsNotNull(messageFromDb);
            Assert.AreEqual(messageFromDb.Id, 1);
            Assert.AreEqual(messageFromDb.UserId, 1);
            Assert.AreEqual(messageFromDb.Text, "anotherText");

            context.Messages.Remove(message);
            context.SaveChanges();
            messageFromDb = context.Messages.Find(1);
            Assert.IsNull(messageFromDb);
        }

        [TestCleanup]
        public void End()
        {
            context?.Dispose();
            if (File.Exists($"{DatabasePath}\\{DatabaseName}"))
                File.Delete($"{DatabasePath}\\{DatabaseName}");
        }
    }
}
