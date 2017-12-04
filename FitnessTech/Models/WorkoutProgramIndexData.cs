using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class WorkoutProgramIndexData
    {
        public IEnumerable<WorkoutProgram>WorkoutPrograms { get; set; }
        public IEnumerable<Workout>Workouts { get; set; }
    }
}
