using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Models
{
    public class Appointment : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }

        public virtual List<Activity> Activities { get; set; }  
    }

}