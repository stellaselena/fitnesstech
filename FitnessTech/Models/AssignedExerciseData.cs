using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class AssignedExerciseData
    {
        public int ExerciseId { get; set; }
        [Display(Name = "Exercise Name")]
        public string ExerciseName { get; set; }
        public bool Assigned { get; set; }
    }
}

