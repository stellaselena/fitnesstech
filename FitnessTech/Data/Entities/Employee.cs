using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Models;

namespace FitnessTech.Data.Entities
{

    public enum Certification
    {
        [Display(Name = "Personal Trainer")]
        PersonalTrainer,
        [Display(Name = "Nutritionist")]
        Nutritionist
    }

    public enum Specialization
    {
        [Display(Name = "Sports Conditioning")]
        SportsConditioning,
        [Display(Name = "Senior Fitness")]
        SeniorFitness,
        [Display(Name = "Youth Fitness")]
        YouthFitness,
        [Display(Name = "Weight Management")]
        WeightManagement

    }
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
