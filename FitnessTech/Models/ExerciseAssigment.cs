using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class ExerciseAssigment
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
