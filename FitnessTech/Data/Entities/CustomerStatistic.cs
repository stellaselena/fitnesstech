using FitnessTech.Models;
using System.ComponentModel.DataAnnotations;

namespace FitnessTech.Data.Entities
{
    public class CustomerStatistic
    {
        [Key]
        public int CustomerStatisticsId { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Display(Name = "Mentor")]
        public Employee ResponsibleEmployee { get; set; }

        public virtual Gym Gym { get; set; }
    }
}
