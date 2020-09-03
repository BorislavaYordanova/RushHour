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
using RushHour.Utility;
using RushHour.Repositories;
using RushHour.ViewModels;
using AutoMapper;

namespace RushHour.Controllers
{
    public class ActivitiesController : Controller
    {
        private RushHourContext _db = new RushHourContext();
        private IBaseRepository<Activity> _repository = null;
       
        public ActivitiesController()
        {
            this._repository = new BaseRepository<Activity>();
        } 

        public ActionResult Index()
        {
            var activities = _repository.SelectAll().ToList();

            var activityViewModel = Mapper.Map<List<ActivityViewModel> >(activities);
            return View(activityViewModel);
        }

        public ActionResult Details(int? id)
        {
            var activities = _repository.SelectByID(id.Value);

            var activityViewModel = Mapper.Map<ActivityViewModel>(activities);
            return View(activityViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ActivityViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityViewModel activityViewModel)
        {
            var activity = new Activity();

            var newActivity = Mapper.Map(activityViewModel, activity);
            _repository.Insert(newActivity);
            _repository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var activity = _repository.SelectByID(id.Value);
            var viewModel = new ActivityViewModel();

            var newActivity = Mapper.Map(activity, viewModel);
            return View(newActivity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ActivityViewModel activityViewModel)
        {
            var activity = _repository.SelectByID(id.Value);
            var newActivity = Mapper.Map(activityViewModel, activity);
            _repository.Update(activity);
            _repository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            if (!LoginUserSession.IsStateAdmin)
            {
               return Redirect("/Users/Login");
            }
            var model = new ActivityViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ActivityViewModel activityViewModel, int? id)
        {
            if (!LoginUserSession.IsStateAdmin)
            {
                return Redirect("/Users/Login");
            }

            var activity = _repository.SelectByID(id.Value);

            var deletedActivity = Mapper.Map(activityViewModel, activity);
            _repository.Delete(deletedActivity);
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
