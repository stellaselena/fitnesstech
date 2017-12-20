namespace FitnessTech.Data.Entities
{
    public class WorkoutAssigment
    {
        public int WorkoutProgramId { get; set; }
        public int WorkoutId { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
        public Workout Workout { get; set; }
    }
}
