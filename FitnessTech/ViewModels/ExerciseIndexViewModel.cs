using FitnessTech.Data.Entities;
using System.Collections.Generic;

namespace FitnessTech.ViewModels
{
    public class ExerciseIndexViewModel
    {
        public ExerciseIndexViewModel()
        {
            Exercises = new List<Exercise>();
        }

        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
