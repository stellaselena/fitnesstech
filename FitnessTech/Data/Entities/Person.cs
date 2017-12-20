using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{
    public abstract class Person
    {

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


    }
}
