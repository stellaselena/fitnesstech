using FitnessTech.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.ViewModels
{
    public class MacroViewModel
    {
        [Required(ErrorMessage = "Age is required.")]
        public double? Age { get; set; }
        [Required(ErrorMessage = "Height is required.")]

        public double? Height { get; set; }
        [Required(ErrorMessage = "Weight is required.")]

        public double? Weight { get; set; }
        [Required(ErrorMessage = "Gender is required.")]

        public Gender? Gender { get; set; }
        [Required(ErrorMessage = "Activity Level is required.")]

        public ActivityLevel? ActivityLevel { get; set; }
        [Required(ErrorMessage = "Goal is required.")]

        public Goal? Goal { get; set; }
        [Required(ErrorMessage = "Age is required.")]

        public Percentage? Percentage { get; set; }
        [Required(ErrorMessage = "Percentage is required.")]

        public int? Carb { get; set; }
        [Required(ErrorMessage = "Protein % is required.")]

        public int? Protein { get; set; }
        [Required(ErrorMessage = "Fat % is required.")]

        public int? Fat { get; set; }
    }
}
