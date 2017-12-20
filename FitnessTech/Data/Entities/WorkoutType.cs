using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{
    public class WorkoutType
    {
        [Key]
        public int WorkoutTypeId { get; set; }

        [Display(Name = "Workout Type Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string WorkoutTypeName { get; set; }

        [Display(Name = "Workout Type Description")]
        public string WorkoutTypeDescription { get; set; }
    }
}
