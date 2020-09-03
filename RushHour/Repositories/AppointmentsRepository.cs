using RushHour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Repositories
{
    public class AppointmentsRepository : BaseRepository<Appointment>
    {
        public List<Activity> GetActivities()
        {
            BaseRepository<Activity> repository = new BaseRepository<Activity>(db);
            List<Activity> activities = repository.SelectAll().ToList();
            return activities;
        }
    }
}