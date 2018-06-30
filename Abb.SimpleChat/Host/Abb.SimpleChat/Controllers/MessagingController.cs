using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Abb.SimpleChat.Business.Logic.Entities;
using Abb.SimpleChat.External.RepositoryEntityFamework;
using Abb.SimpleChat.Infrastructure.Logger;
using System.Threading.Tasks;
using Abb.SimpleChat.Hub;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Abb.SimpleChat.Controllers
{
    public class MessagingController : Controller
    {
        IHubContext<UpdateChatHub, ITypedHubClient> chatHubContext;
        private const string Path =
           "C:\\Users\\Сергей\\source\\repos\\Abb.SimpleChat\\Host\\Abb.SimpleChat";
        private const string DatabaseName = "Base.db3";
        private const string fileName = "file.txt";
        private Messages message;
        private Users user;
        private int i, messageId;
        private string otvet;
        private DataColumn column;
        private DataRow row;
        public DataTable messageTable;
        private DatabaseSettings databaseSettings;
        SimpleChatRepository<Users> userRepository;
        SimpleChatRepository<Messages> messageRepository;
        NLogLogger log;

        public MessagingController(IHubContext<UpdateChatHub, ITypedHubClient> chatHubContext)
        {
            try
            {
                this.chatHubContext = chatHubContext;

                log = new NLogLogger($"{Path}\\{fileName}");
                databaseSettings = new DatabaseSettings();
                databaseSettings.ConnnectionString = $"{Path}\\{DatabaseName}";
                messageRepository = new SimpleChatRepository<Messages>(databaseSettings);
                userRepository = new SimpleChatRepository<Users>(databaseSettings);
                messageTable = new DataTable("MessageTable");
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "NameUser";
                column.Caption = "NameUser";
                column.AutoIncrement = false;
                column.ReadOnly = false;
                column.Unique = false;
                messageTable.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Messages";
                column.Caption = "Messages";
                column.AutoIncrement = false;
                column.ReadOnly = false;
                column.Unique = false;
                messageTable.Columns.Add(column);
                ShowMessages();
                
            }
            catch (Exception e)
            {
                log.Error("Ошибка настройки", e);
            }
        }

        [HttpGet]
        public IActionResult MessagingView(string name, int id)
        {
            
            user = new Users()
            {
                Name = name,
                Id = id
            };

            ViewBag.user = user;
            ViewBag.messageTable = messageTable;
            
            return View();
        }

        [HttpPost]
        public IActionResult MessagingView(string message, Users user)
        {
            WriteMessage(message, user.Id);
            ViewBag.user = user;
            ViewBag.messageTable = messageTable;
            return View();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            chatHubContext.Clients.All.BroadcastMessage(message.Text, user.Name);
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public string WriteMessage(string UserMessage, int userId)
        {
            try
            {
                messageRepository.CreatDatabase();

                message = new Messages();

                message.Text = UserMessage;
                message.UserId = userId;

                messageRepository.Add(message);
                messageRepository.Save();


                Get();
         

                otvet = "Сообщение доставлено";
                log.Info($"Доставлено сообщение от {userId}");
            }
            catch (Exception e)
            {
                otvet = "Сообщение не доставлено";
                log.Error(otvet, e);

            }
            return otvet;
        }

        [HttpPost]
        public void ShowMessages()
        {
            try
            {
               
                messageTable.Rows.Clear();
                messageId = messageRepository.Count();
                i = 1;
                if (messageId < 11)
                    while (i <= messageId)
                    {
                        var messageFromDb = messageRepository.GetItem(i);
                        var userFromDb = userRepository.GetItem(messageFromDb.UserId);
                        row = messageTable.NewRow();
                        row["NameUser"] = userFromDb.Name;
                        row["Messages"] = messageFromDb.Text;
                        messageTable.Rows.Add(row);
                        i++;
                    }
                else
                {
                    i = messageId - 9;
                    while (i <= messageId)
                    {
                        var messageFromDb = messageRepository.GetItem(i);
                        var userFromDb = userRepository.GetItem(messageFromDb.UserId);
                        row = messageTable.NewRow();
                        row["NameUser"] = userFromDb.Name;
                        row["Messages"] = messageFromDb.Text;
                        messageTable.Rows.Add(row);
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Ошибка показа сообщений", e);
            }
        }
    }
}