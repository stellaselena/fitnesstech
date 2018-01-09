using System.Collections.Generic;
using FitnessTech.Data.Entities;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;
using Microsoft.AspNetCore.Http;

namespace FitnessTech.ViewModels
{
    public class ExerciseViewModel
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
        [Display(Name = "Choose an image")]
        [Required(ErrorMessage = "Image is required")]

        public IFormFile AvatarImage { get; set; }
    }
}
