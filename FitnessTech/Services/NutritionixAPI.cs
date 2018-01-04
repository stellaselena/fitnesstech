using System;
using System.Collections.Generic;
using Nutritionix;

namespace FitnessTech.Services
{
    public class NutritionixApi
    {
        private static readonly string AppId;
        private static readonly string AppKey;

        static NutritionixApi()
        {
            AppId = "972c3f44";
            AppKey = "59c32e8808fd73a6078b3c0aaba19a8c";
        }

        public static ICollection<string> SearchItems(string searchString)
        {
            var nutritionix = new NutritionixClient();
            nutritionix.Initialize(AppId, AppKey);
            var request = new SearchRequest { Query = searchString };
            
            SearchResponse response = nutritionix.SearchItems(request);
            List< string> searchResults = new List<string>();
            foreach (SearchResult result in response.Results)
            {
                searchResults.Add(result.Item.Name);             
            }

            return searchResults;
        }

        public static ICollection<Item> PowerSearchItems(string foodName, string brandName, bool isRestaurant, bool isPackaged, bool isUsda)
        {    
            var nutritionix = new NutritionixClient();
            nutritionix.Initialize(AppId, AppKey);
            var request = new PowerSearchRequest();
            request.Queries = new QueryFilterCollection();
            if (!String.IsNullOrEmpty(foodName))
            {
                request.Queries.Add(new QueryFilter(x => x.Name, foodName));
                          
            }           
            if (!String.IsNullOrEmpty(brandName))
            {
                request.Queries.Add(new QueryFilter(x => x.BrandName, brandName));
              
            }

            request.Fields = new SearchResultFieldCollection
            {
                x => x.Name,
                x => x.BrandName,
                x => x.NutritionFact_Calories,
                x => x.NutritionFact_TotalCarbohydrate,
                x => x.NutritionFact_Protein,
                x => x.NutritionFact_TotalFat,
                x => x.NutritionFact_Sugar,
                x => x.NutritionFact_DietaryFiber,
                x => x.ItemType
            };


            request.SortBy = new SearchResultSort(x => x.NutritionFact_Calories, SortOrder.Descending);


            request.Filters = new SearchFilterCollection();
            if (isRestaurant)
            {
                request.Filters.Add(new ItemTypeFilter {Negated = false, ItemType = ItemType.Restaurant});

            }
            if (isPackaged)
            {
                request.Filters.Add(new ItemTypeFilter { Negated = false, ItemType = ItemType.Packaged });

            }
            if (isUsda)
            {
                request.Filters.Add(new ItemTypeFilter { Negated = false, ItemType = ItemType.USDA });

            }
            SearchResponse response = nutritionix.SearchItems(request);
            List<Item> searchResults = new List<Item>();
           
            foreach (SearchResult result in response.Results)
            {
                searchResults.Add(new Item()
                {

                    BrandName = result.Item.BrandName,
                    Name = result.Item.Name,
                    NutritionFact_Calories = result.Item.NutritionFact_Calories,
                    NutritionFact_Sugar = result.Item.NutritionFact_Sugar,
                    NutritionFact_TotalCarbohydrate = result.Item.NutritionFact_TotalCarbohydrate,
                    NutritionFact_Protein = result.Item.NutritionFact_Protein,
                    NutritionFact_TotalFat = result.Item.NutritionFact_TotalFat,
                    NutritionFact_DietaryFiber = result.Item.NutritionFact_DietaryFiber,
                    ItemType = result.Item.ItemType
                });
               
            }

            return searchResults;

        }
    }
}
