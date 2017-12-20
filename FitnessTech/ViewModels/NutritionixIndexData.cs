using System.Collections.Generic;

namespace FitnessTech.ViewModels
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
