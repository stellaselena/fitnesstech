namespace FitnessTech.Data.Entities
{
    public class Macro
    {
        public double Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public Goal Goal { get; set; }
        public Percentage Percentage { get; set; }
        public int Carb { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }
}
