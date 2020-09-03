using RushHour.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RushHour.ViewModels
{
    public class AppointmentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "User Id")]
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter Start Date Time.")]
        [Display(Name = "Start Datetime")]
        public DateTime StartDateTime { get; set; }
        [Required(ErrorMessage = "Please enter End Date Time.")]
        [Display(Name = "End Datetime")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Please, select activity.")]
        [Display(Name = "Activities")]
        public virtual IEnumerable<SelectListItem> ActivitiesSelectList { get; set; }
    }
}