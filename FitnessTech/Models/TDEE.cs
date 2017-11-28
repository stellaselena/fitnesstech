using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class TDEE
    {
        public double Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }

        public ActivityLevel ActivityLevel { get; set; }
    }
}
