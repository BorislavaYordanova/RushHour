using RushHour.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RushHour.ViewModels
{
    public class ActivityViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, enter name.")]
        [Display(Name = "Activity Name")]
        [MaxLength(100, ErrorMessage = "The name is too long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter the duration.")]
        [Display(Name = "Duration")]
        public float Duration { get; set; }
        [Required(ErrorMessage = "Please, enter the price.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public  List<Appointment> Appointments { get; set; }


    }
}