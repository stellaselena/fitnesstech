using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
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
        [Required]
        public Certification Certification { get; set; }

        [Required]
        public Specialization Specialization { get; set; }

        public virtual Gym Gym { get; set; }
    }
}
