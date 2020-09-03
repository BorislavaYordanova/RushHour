using RushHour.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RushHour.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter User Name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
        [Display(Name = "Is Admin?")]
        public bool IsAdmin { get; set; }
    }
}