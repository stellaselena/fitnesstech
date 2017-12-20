using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
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

        public ICollection<WorkoutAssigment> WorkoutAssigments { get; set; }
    }
}
