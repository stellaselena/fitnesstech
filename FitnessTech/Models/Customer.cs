using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public enum Goal
    {
        Weightloss,
        Maintenance,
        [Display(Name = "Weight gain")]
        WeightGain,
        [Display(Name = "Muscle gain")]
        Musclegain
    }

    public enum ActivityLevel
    {
        Sedentary,
        Light,
        Moderate,
        Heavy,
        Athlete
    }
    public class Customer : Person
    {
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
    }
}
