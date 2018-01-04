using FitnessTech.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;

namespace FitnessTech.ViewModels
{
    public class ExerciseIndexViewModel
    {
        public ExerciseIndexViewModel()
        {
            Exercises = new List<Exercise>();
        }

        public IEnumerable<Exercise> Exercises { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        [Display(Name = "Legs")]
        public bool Legs { get; set; }
        [Display(Name = "Back")]
        public bool Back { get; set; }
        [Display(Name = "Chest")]
        public bool Chest { get; set; }
        [Display(Name = "Shoulders")]
        public bool Shoulders { get; set; }
        [Display(Name = "Arms")]
        public bool Arms { get; set; }
        [Display(Name = "Abdominals")]
        public bool Abdominals { get; set; }


    }
}
