using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Business_Logic;
using FitnessTech.Models;
using Microsoft.AspNetCore.Mvc;
using Nutritionix;

namespace FitnessTech.Controllers
{
    public class NutritionController : Controller
    {
        public IActionResult Index()
        {
            return View("NutritionIndex");
        }

        public IActionResult SearchFood(string searchStringFood, string searchStringBrand, string sortOrder)
        {
            
           
            var viewModel = new NutritionixIndexData();
            if (!String.IsNullOrEmpty(searchStringBrand) || !String.IsNullOrEmpty(searchStringFood))
            {
                var result = NutritionixApi.PowerSearchItems(searchStringFood, searchStringBrand);
                foreach (var item in result)
                {
                    viewModel.NutritionixItems.Add(new NutritionixItem()
                    {
                        Name = item.Name,
                        BrandName = item.BrandName,
                        NutritionFact_Calories = item.NutritionFact_Calories,
                        NutritionFact_Protein = item.NutritionFact_Protein,
                        NutritionFact_TotalCarbohydrate = item.NutritionFact_TotalCarbohydrate,
                        NutritionFact_DietaryFiber = item.NutritionFact_DietaryFiber,
                        NutritionFact_Sugar = item.NutritionFact_Sugar,
                        NutritionFact_TotalFat = item.NutritionFact_TotalFat,
                        NutritionFact_CaloriesFromFat = item.NutritionFact_CaloriesFromFat,
                        ItemType = item.ItemType
                    });
                }
            }

            return View(viewModel);
        }
    }
}