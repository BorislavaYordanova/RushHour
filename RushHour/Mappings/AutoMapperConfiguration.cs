using AutoMapper;
using RushHour.Models;
using RushHour.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<Activity, ActivityViewModel>();
                cfg.CreateMap<ActivityViewModel, Activity>();
                cfg.CreateMap<Appointment, AppointmentViewModel>();
                cfg.CreateMap<AppointmentViewModel, Appointment>();
            });
        }
    }
}