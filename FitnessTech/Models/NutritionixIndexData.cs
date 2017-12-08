using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Models
{
    public class NutritionixIndexData
    {
        public NutritionixIndexData()
        {
            NutritionixItems = new List<NutritionixItem>();
        }

        public ICollection<NutritionixItem> NutritionixItems { get; set; }
    }
}
