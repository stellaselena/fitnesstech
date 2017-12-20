using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.ViewModels
{
    public class WorkoutIndexData
    {
        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
