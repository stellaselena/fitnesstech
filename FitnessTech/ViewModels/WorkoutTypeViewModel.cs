using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.ViewModels
{
    public class WorkoutTypeViewModel
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
