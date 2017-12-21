using FitnessTech.Data.Entities;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;

namespace FitnessTech.ViewModels
{
    public class BMRViewModel
    {
        [Required(ErrorMessage = "Age is required.")]
        public double? Age { get; set; }
        [Required(ErrorMessage = "Height is required.")]
        public double? Height { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        public double? Weight { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public Gender? Gender { get; set; }
    }
}
