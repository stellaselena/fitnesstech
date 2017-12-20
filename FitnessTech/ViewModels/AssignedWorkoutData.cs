using System.ComponentModel.DataAnnotations;

namespace FitnessTech.ViewModels
{
    public class AssignedWorkoutData
    {
        public int WorkoutId { get; set; }
        [Display(Name = "Workout Name")]
        public string WorkoutName { get; set; }
        public bool Assigned { get; set; }
    }
}
