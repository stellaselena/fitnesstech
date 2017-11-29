using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Business_Logic;
using FitnessTech.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTech.Controllers
{
    public class MacroCalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View("MacroCalculator");
        }


        [HttpPost]
        public IActionResult Index(Macro macro)
        {
            if (!ModelState.IsValid) return View("MacroCalculator");
            var macroResult = Calculator.MacroCalculator(macro);
            var macroNutrients = Calculator.CalculateMacronutrients(macro, macro.Carb, macro.Protein, macro.Fat);
            if (macroNutrients == null)
            {
                ViewBag.ErrorMessage = "Macronutrient sum must be 100";
                return View("MacroCalculator");

            }
            ViewBag.Result = Convert.ToInt32(macroResult);
            ViewBag.Carbs = Convert.ToInt32(macroNutrients["carbs"]);
            ViewBag.Proteins = Convert.ToInt32(macroNutrients["proteins"]);
            ViewBag.Fats = Convert.ToInt32(macroNutrients["fats"]);

            return View("MacroCalculator");
        }
    }
}