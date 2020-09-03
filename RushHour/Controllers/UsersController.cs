using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RushHour.DataAccess;
using RushHour.Models;
using RushHour.ViewModels;
using RushHour.Utility;
using RushHour.Repositories;
using AutoMapper;
using RushHour.Mappings;
using System.Security.Cryptography;

namespace RushHour.Controllers
{
    public class UsersController : Controller
    {
        private RushHourContext _db = new RushHourContext();
        private IBaseRepository<User> _repository = null;

        public UsersController()
        {
            this._repository = new BaseRepository<User>();
        }

        public ActionResult Login()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ModelState.Clear();

            var hashPasswoed = ShaPassword.GetHash(model.Password);
            var user = _db.Users.Where(x => x.Name == model.Name && x.Password == hashPasswoed).FirstOrDefault();
            if (user != null)
            {
                LoginUserSession.SetSessionAndVote(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid name or password. Please, try again";
                return View(model);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}


        public ActionResult Index()
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var users = _repository.SelectAll().ToList();
            var userViewModel = Mapper.Map<List<UserViewModel>>(users);

            return View(userViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var users = _repository.SelectByID(id.Value);
            var userViewModel = Mapper.Map<UserViewModel>(users);

            return View(userViewModel);
        }

        public ActionResult Create()
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var user = new User();
            userViewModel.Password = ShaPassword.GetHash(userViewModel.Password);
            var newUser = Mapper.Map(userViewModel, user);
            _repository.Insert(newUser);
            _repository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var user = _repository.SelectByID(id.Value);
            var viewModel = new UserViewModel();
            var newUser = Mapper.Map(user, viewModel);

            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, UserViewModel userViewModel)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            userViewModel.Password = ShaPassword.GetHash(userViewModel.Password);
            var user = _repository.SelectByID(id.Value);
            var newUser = Mapper.Map(userViewModel, user);
            _repository.Update(user);
            _repository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel userViewModel, int? id)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return RedirectToAction("Login");
            }
            var user = _repository.SelectByID(id.Value);            
            var deletedUser = Mapper.Map(userViewModel, user);
            _repository.Delete(deletedUser);
            _repository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
