using System;
using FitnessTech.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using FitnessTech.Models;


namespace FitnessTech.Controllers
{
    public class TDEECalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View("TDEECalculator");
        }


        [HttpPost]
        public IActionResult Index(TDEE tdee)
        {
            if (!ModelState.IsValid) return View("TDEECalculator");
            var tdeeResult = Calculator.TdeeCalculator(tdee);
            ViewBag.Result = Convert.ToInt32(tdeeResult);

            return View("TDEECalculator");
        }
    }
}