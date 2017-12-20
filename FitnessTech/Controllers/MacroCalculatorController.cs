using AutoMapper;
using FitnessTech.Business_Logic;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FitnessTech.Controllers
{
    public class MacroCalculatorController : Controller
    {
        private readonly IMapper _mapper;

        public MacroCalculatorController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View("MacroCalculator");
        }


        [HttpPost]
        public IActionResult Index(MacroViewModel model)
        {
            if (!ModelState.IsValid) return View("MacroCalculator");
            var macro = _mapper.Map<MacroViewModel, Macro>(model);
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
