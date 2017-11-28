using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var bmrResult = 0.0;
            if (bmi.Gender == Gender.Male)
            {
                bmrResult = 66 + (13.7 * bmi.Weight) + (5 * bmi.Height) - (6.8 * bmi.Age);
            }
            else
            {
                bmrResult = 655 + (9.6 * bmi.Weight) + (1.8 * bmi.Height) - (4.7 * bmi.Age);
            }

                
            ViewBag.Result = Convert.ToInt32(bmrResult);

            return View("BMRCalculator");
        }
    }
}