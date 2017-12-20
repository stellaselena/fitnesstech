using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;

namespace FitnessTech.ViewModels
{
    public class WorkoutProgramViewModel
    {
        [Key]
        public int WorkoutProgramId { get; set; }

        [Display(Name = "Workout Program Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string WorkoutProgramName { get; set; }

        [Display(Name = "Workout Program Description")]
        public string WorkoutProgramDescription { get; set; }

        public ICollection<WorkoutAssigment> WorkoutAssigments { get; set; }
    }
}
