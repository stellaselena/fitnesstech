using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Models;

namespace FitnessTech.Models
{
    public enum Percentage
    {
        Suggested,
        Aggressive,
        Reckless
       
    }
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
