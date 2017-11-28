using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Business_Logic;
using FitnessTech.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTech.Controllers
{
    public class BMRCalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View("BMRCalculator");
        }

        [HttpPost]
        public IActionResult Index(BMI bmi)
        {
       
            if (!ModelState.IsValid) return View("BMRCalculator");
            var bmrResult = Calculator.BmrCalculator(bmi);
            ViewBag.Result = Convert.ToInt32(bmrResult);

            return View("BMRCalculator");
        }

       
    }
}