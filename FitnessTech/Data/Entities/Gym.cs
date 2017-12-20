using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{
    public class Gym
    {
        [Key]
        public int GymId { get; set; }

        [Display(Name = "Gym name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string GymName { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Display(Name = "Zip Code")]
        public int PostalCode { get; set; }

        public string Street { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

    }
}
