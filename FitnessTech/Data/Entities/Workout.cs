using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{

    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        [Display(Name = "Workout Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string WorkoutName { get; set; }
        //[Required(ErrorMessage = "Workout Type is required")]
        [Display(Name = "Workout Type")]
        public virtual WorkoutType WorkoutType { get; set; }
        public int? WorkoutTypeId { get; set; }
        [Display(Name = "Exercises")]
        public ICollection<ExerciseAssigment> ExerciseAssigments { get; set; }



    }
}
