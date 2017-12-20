using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.ViewModels
{
    public class WorkoutProgramIndexData
    {
        public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }
    }
}
