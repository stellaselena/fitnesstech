using Nutritionix;

namespace FitnessTech.ViewModels
{
    public class NutritionixItem
    {
        public string Id { get; set; }
        public decimal? NutritionFact_Calories { get; set; }
        public decimal? NutritionFact_DietaryFiber { get; set; }
        public decimal? NutritionFact_Protein { get; set; }
        public decimal? NutritionFact_TotalCarbohydrate { get; set; }
        public decimal? NutritionFact_Sugar { get; set; }
        public decimal? NutritionFact_TotalFat { get; set; }
        public decimal? NutritionFact_CaloriesFromFat { get; set; }
        public decimal? NutritionFact_ServingGramWeight { get; set; }
        public string BrandName { get; set; }
        public ItemType ItemType { get; set; }
        public string Name { get; set; }



    }
}
