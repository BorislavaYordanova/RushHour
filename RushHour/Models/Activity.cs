using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Models
{
    public class Activity: BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float Duration { get; set; }
        [Required]
        public decimal Price { get; set; }

        public virtual List<Appointment> Appointments { get; set; }
    }
}