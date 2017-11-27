using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
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
