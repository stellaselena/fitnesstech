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

        public static ICollection<Item> PowerSearchItems(string foodName, string brandName)
        {
            
            //ItemType type = ItemType.Packaged;
            //switch (itemType)
            //{
            //    case "packaged":
            //        type = ItemType.Packaged;
            //        break;
            //    case "restaurant":
            //        type = ItemType.Restaurant;
            //        break;
            //    case "USDA":
            //        type = ItemType.USDA;
            //        break;

            //}
            var nutritionix = new NutritionixClient();
            nutritionix.Initialize(AppId, AppKey);

            PowerSearchRequest request;
            if (!String.IsNullOrEmpty(brandName) && !String.IsNullOrEmpty(foodName))
            {
                request = new PowerSearchRequest
                {

                    Queries = new QueryFilterCollection
                    {
                        new QueryFilter(x => x.Name, foodName),
                        new QueryFilter(x => x.BrandName, brandName)
                    },
                    Fields = new SearchResultFieldCollection
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
                    },
                    SortBy = new SearchResultSort(x => x.NutritionFact_Calories, SortOrder.Descending),
                    //Filters = new SearchFilterCollection
                    //{
                    //    new ItemTypeFilter {Negated = negated, ItemType = type}
                    //}
                };
            }
            else if (!String.IsNullOrEmpty(brandName) && String.IsNullOrEmpty(foodName))
            {
                request = new PowerSearchRequest
                {

                    Queries = new QueryFilterCollection
                    {
                        new QueryFilter(x => x.BrandName, brandName)
                    },
                    Fields = new SearchResultFieldCollection
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
                    },
                    SortBy = new SearchResultSort(x => x.NutritionFact_Calories, SortOrder.Descending),
                    //Filters = new SearchFilterCollection
                    //{
                    //    new ItemTypeFilter {Negated = negated, ItemType = type}
                    //}
                };
            }
            else
            {
                request = new PowerSearchRequest
                {

                    Queries = new QueryFilterCollection
                    {
                        new QueryFilter(x => x.Name, foodName)
                    },
                    Fields = new SearchResultFieldCollection
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
                    },
                    SortBy = new SearchResultSort(x => x.NutritionFact_Calories, SortOrder.Descending),
                    //Filters = new SearchFilterCollection
                    //{
                    //    new ItemTypeFilter {Negated = negated, ItemType = type}
                    //}
                };
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
