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
    public class AppointmentsController : Controller
    {
        private RushHourContext _db = new RushHourContext();
        private AppointmentsRepository _repository;

        public AppointmentsController()
        {
            this._repository = new AppointmentsRepository();
        }

        public ActionResult Index()
        {
            var appointments = _repository.SelectAll().ToList();
            var appointmentViewModel = Mapper.Map<List<AppointmentViewModel>>(appointments);
            
            if (LoginUserSession.Current != null)
            {
                return View(appointmentViewModel);
            }
            return Redirect("/Users/Login");
        }

        public ActionResult Details(int? id)
        {
            var appointments = _repository.SelectByID(id.Value);
            var appointmentViewModel = Mapper.Map<AppointmentViewModel>(appointments);

            return View(appointmentViewModel);
        }

        public ActionResult Create()
        {
            var appointmentModel = new AppointmentViewModel();
            var activities = new BaseRepository<Activity>().SelectAll();
            var activityModel = new ActivityViewModel();
            appointmentModel.ActivitiesSelectList = GetSelectedListItem(activities);

            return View(appointmentModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel appointmentViewModel, string[] selectedActivities)
        {
            var appointment = new Appointment();
            var newAppointment = Mapper.Map(appointmentViewModel, appointment);
            newAppointment.UserId = Utility.LoginUserSession.Current.UserId;
            newAppointment.Activities = GetSelectedActivities(selectedActivities);
            _repository.Insert(newAppointment);
            _repository.Save();
            
            return RedirectToAction("Index");
        }

        private List<Activity> GetSelectedActivities(string[] selectedActivities)
        {
            List<Activity> result = new List<Activity>();
            var allActivities = _repository.GetActivities();
            foreach (var item in selectedActivities)
            {
                foreach(var activity in allActivities)
                {
                    if (activity.Id.ToString() == item)
                    {
                        result.Add(activity);
                    }
                } 
            }
            return result;
        }

        private IEnumerable<SelectListItem> GetSelectedListItem(IEnumerable<Activity> elements)
        {
            var selectlist = new List<SelectListItem>(); 
            foreach (var element in elements)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = element.Name,
                    Value = element.Id.ToString(),
                    Selected = false
                });
            }
            return selectlist;
        }

        public ActionResult Edit(int? id)
        {
            var appointment = _repository.SelectByID(id.Value);
            var viewModel = new AppointmentViewModel();
            var newAppointment = Mapper.Map(appointment, viewModel);

            return View(newAppointment);

            //    Appointment app = Service.Get(a => a.Id == id);
            //    AppointmentVM appVM = new AppointmentVM();
            //    Mapper.Map(app, appVM);
            //    appVM.Activities = Mapper.Map<List<ActivityVM>>(activityService.GetAll());

            //    foreach (Activity act in app.Activities)
            //    {
            //        appVM.Activities.Find(a => a.Name == act.Name).IsChecked = true;
            //    }
            //    return View(appVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, AppointmentViewModel appointmentViewModel)
        {
            var appointment = _repository.SelectByID(id.Value);
            var newAppointment = Mapper.Map(appointmentViewModel, appointment);
            _repository.Update(appointment);
            _repository.Save();

            return View(appointmentViewModel);
        }

        public ActionResult Delete(int? id)
        {
            var appointment = _repository.SelectByID(id.Value);
            var viewModel = new AppointmentViewModel();
            var deletedAppointment = Mapper.Map(appointment, viewModel);

            return View(deletedAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, AppointmentViewModel appointmentViewModel)
        {
            var appointment = _repository.SelectByID(id.Value);
            var newAppointment = Mapper.Map(appointmentViewModel, appointment);
            _repository.Delete(newAppointment);
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
