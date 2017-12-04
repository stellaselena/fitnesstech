using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class WorkoutAssigment
    {
        public int WorkoutProgramId { get; set; }
        public int WorkoutId { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
        public Workout Workout { get; set; }
    }
}
