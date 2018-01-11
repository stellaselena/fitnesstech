using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FitnessTech.Data.Entities
{
   
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
        public byte[] AvatarImage { get; set; }

        public string Equipment { get; set; }
        public Level Level { get; set; }
        public Mechanics Mechanics { get; set; }


    }
}
