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
            ViewBag.Result = Convert.ToInt32(macroResult);

            return View("MacroCalculator");
        }
    }
}