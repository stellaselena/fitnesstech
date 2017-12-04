using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
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
        public MuscleGroup MuscleGroup { get; set; }


    }
}
