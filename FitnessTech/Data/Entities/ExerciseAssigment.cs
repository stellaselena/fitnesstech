namespace FitnessTech.Data.Entities
{
    public class ExerciseAssigment
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
