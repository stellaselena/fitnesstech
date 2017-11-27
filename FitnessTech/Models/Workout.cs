using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
  
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        [Display(Name = "Workout Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string WorkoutName { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

        public virtual WorkoutType WorkoutType { get; set; }
    }
}
