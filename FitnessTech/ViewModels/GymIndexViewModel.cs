using FitnessTech.Data.Entities;
using System.Collections.Generic;

namespace FitnessTech.ViewModels
{
    public class GymIndexViewModel
    {
        public GymIndexViewModel()
        {
            Gyms = new List<Gym>();
        }

        public IEnumerable<Gym> Gyms { get; set; }
    }
}
