using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class AssignedWorkoutData
    {
        public int WorkoutId { get; set; }
        [Display(Name = "Workout Name")]
        public string WorkoutName { get; set; }
        public bool Assigned { get; set; }
    }
}
