using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RushHour.DataAccess;
using RushHour.Models;

namespace RushHour.DataAccess
{
    public class RushHourContext : DbContext
    {
        public RushHourContext() : base("RushHourDb")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}