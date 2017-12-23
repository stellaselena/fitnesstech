using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;

namespace FitnessTech.Data.Entities
{

    public class Employee : Person
    {
        [Required(ErrorMessage = "Certification is required")]
        public Certification Certification { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public Specialization Specialization { get; set; }
        public virtual Gym Gym { get; set; }
        public int? GymId { get; set; }
        public virtual ICollection<Employee> Customers { get; set; }



    }
}
