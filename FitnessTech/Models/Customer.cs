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

        public int Weight { get; set; }

        public int Height { get; set; }

        [Display(Name = "Activity level")]
        public ActivityLevel ActivityLevel { get; set; }

        [Display(Name = "Body fat percentage")]
        public int BodyFat { get; set; }
    }
}
