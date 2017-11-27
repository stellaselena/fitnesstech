using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class WorkoutProgram
    {
        [Key]
        public int WorkoutProgramId { get; set; }

        [Display(Name = "Workout Program Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string WorkoutProgramName { get; set; }

        [Display(Name = "Workout Program Description")]
        public string WorkoutProgramDescription { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
        
    }
}
