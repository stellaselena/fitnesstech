using System.Collections.Generic;

namespace FitnessTech.Models
{
    public class WorkoutIndexData
    {
        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
