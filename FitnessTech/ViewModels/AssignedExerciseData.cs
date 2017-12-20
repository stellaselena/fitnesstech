using System.ComponentModel.DataAnnotations;

namespace FitnessTech.ViewModels
{
    public class AssignedExerciseData
    {
        public int ExerciseId { get; set; }
        [Display(Name = "Exercise Name")]
        public string ExerciseName { get; set; }
        public bool Assigned { get; set; }
    }
}
