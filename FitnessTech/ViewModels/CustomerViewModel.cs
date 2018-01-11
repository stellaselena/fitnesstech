using FitnessTech.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;
using Microsoft.AspNetCore.Http;

namespace FitnessTech.ViewModels
{
    public class CustomerViewModel
    {
        //inherited from person
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]

        public string FullName => FirstName + " " + LastName;

        [Display(Name = "Birthdate")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public Goal Goal { get; set; }
        [Range(0, 300)]
        public int Weight { get; set; }
        [Range(0, 250)]
        public int Height { get; set; }

        [Display(Name = "Activity level")]
        public ActivityLevel ActivityLevel { get; set; }

        [Display(Name = "Body fat percentage")]
        [Range(0, 50)]
        public int BodyFat { get; set; }
        public virtual Gym Gym { get; set; }
        public int? GymId { get; set; }
        public virtual Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
        [Display(Name = "Choose an image")]

        public IFormFile AvatarImage { get; set; }
    }
}
