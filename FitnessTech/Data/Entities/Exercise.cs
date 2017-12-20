using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{
    public enum MuscleGroup
    {
        Legs,
        Back,
        Chest,
        Shoulders,
        Arms,
        Abdominals
    }
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Display(Name = "Exercise Name")]
        public string ExerciseName { get; set; }

        [Display(Name = "Exercise Description")]
        public string ExerciseDescription { get; set; }

        [Display(Name = "Muscle group")]
        [Required(ErrorMessage = "Muscle Group is required")]

        public MuscleGroup MuscleGroup { get; set; }


    }
}
