using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;

namespace FitnessTech.ViewModels
{
    public class WorkoutTypeIndexData
    {
        public WorkoutTypeIndexData()
        {
            WorkoutTypes = new List<WorkoutType>();
        }

        public IEnumerable<WorkoutType> WorkoutTypes { get; set; }
    }
}
