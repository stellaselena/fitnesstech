using System.ComponentModel.DataAnnotations;
using FitnessTech.Data.Helpers;

namespace FitnessTech.Data.Entities
{
  
    public class Customer : Person
    {
        public Goal Goal { get; set; }
        [Range(0, 300)]
        public int Weight { get; set; }
        [Range(0, 250)]
        public int Height { get; set; }

        [Display(Name = "Activity level")]
        public ActivityLevel ActivityLevel { get; set; }

        [Display(Name = "Body fat percentage")]
        [Range(0, 50)]
        public int BodyFat { get; set; }
        public virtual Gym Gym { get; set; }
        public int? GymId { get; set; }
        public virtual Employee Employee { get; set; }
        public int? EmployeeId { get; set; }
        public byte[] AvatarImage { get; set; }

    }
}
