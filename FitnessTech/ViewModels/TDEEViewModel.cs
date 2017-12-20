using FitnessTech.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.ViewModels
{
    public class TDEEViewModel
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
    }
}
