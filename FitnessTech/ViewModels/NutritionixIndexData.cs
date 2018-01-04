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
        public bool IsRestaurant { get; set; }
        public bool IsUsda { get; set; }
        public bool IsPackaged { get; set; }
    }
}
