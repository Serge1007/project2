using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abb.SimpleChat.Business.Logic.Entities;
using Abb.SimpleChat.External.RepositoryEntityFamework;
using Abb.SimpleChat.Infrastructure.Logger;

namespace Abb.SimpleChat.Controllers
{
    public class AuthorizationController : Controller
    {
        private const string Path =
            "C:\\Users\\Сергей\\source\\repos\\Abb.SimpleChat\\Host\\Abb.SimpleChat";
        private const string DatabaseName = "Base.db3";
        private const string fileName = "file.txt";
        private Users user;
        private int i;
        private string otvet;
        private DatabaseSettings databaseSettings;
        SimpleChatRepository<Users> userRepository;
        NLogLogger log;

        // GET: Authorization
        [HttpGet]
        public IActionResult AuthorizationView()
        {
            return View();
        }

        public void SetSettings()
        {
            try
            {
                log = new NLogLogger($"{Path}\\{fileName}");
                databaseSettings = new DatabaseSettings();
                databaseSettings.ConnnectionString = $"{Path}\\{DatabaseName}";
            }
            catch(Exception e)
            {
                log.Error("Ошибка настройки", e);
            }
        }
        
        [HttpPost]
        public IActionResult AuthorizationView(string name, string pass, string action)
        {

            if (action == "Authorization")
            {
                Authorization(name, pass);
            }
            else if (action == "Registration")
            {
                Registration(name, pass);
                
            }
            if(otvet == "Пользователь авторизован")
                return RedirectToAction("MessagingView", "Messaging", new { user.Name, user.Id });
            else
                return View();
        }

        [HttpPost]
        public string Registration(string name, string pass)
        {
            try
            {
                SetSettings();
                userRepository = new SimpleChatRepository<Users>(databaseSettings);
                userRepository.CreatDatabase();
                user = new Users();

                user.Name = name;
                user.Pass = Convert.ToInt32(pass);

                userRepository.Add(user);
                userRepository.Save();

                otvet = "Пользователь зарегистрирован";
                log.Info($"Пользователь {name} зарегистрирован");
            }
            catch(Exception e)
            {
                otvet = "Ошибка регистрации";
                log.Error(otvet, e);

            }
            return otvet;
        }

        [HttpPost]
        public string Authorization(string name, string pass)
        {
            try
            {
                SetSettings();
                userRepository = new SimpleChatRepository<Users>(databaseSettings);
                userRepository.CreatDatabase();
                user = new Users();

                i = 0;
                while (i < userRepository.Count())
                {
                    i++;
                    var userFromDb = userRepository.GetItem(i);
                    if (userFromDb.Name == name) { user = userFromDb; break; }
                }
                if (user==null) otvet = "Пользователь не найден";
                else if (user.Pass == Convert.ToInt32(pass))
                {
                    otvet = "Пользователь авторизован";
                    log.Info($"Пользователь {name} авторизован");
                }
                else
                {
                    otvet = "Пароль неверный";
                    log.Info($"Попытка авторизации {name} не удалась");
                }
            }
            catch (Exception e)
            {
                otvet = "Ошибка авторизации";
                log.Error(otvet, e);
            }
            return otvet;
        }

        



       

        // GET: Abb/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Abb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Abb/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(AuthorizationView));
            }
            catch
            {
                return View();
            }
        }

        // GET: Abb/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Abb/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(AuthorizationView));
            }
            catch
            {
                return View();
            }
        }

        // GET: Abb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Abb/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(AuthorizationView));
            }
            catch
            {
                return View();
            }
        }
    }
}