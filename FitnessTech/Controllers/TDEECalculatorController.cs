using System;
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
            double activityRate;
            var activityLevel = tdee.ActivityLevel;
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    activityRate = 1.2;
                    break;
                case ActivityLevel.Light:
                    activityRate = 1.375;
                    break;
                case ActivityLevel.Moderate:
                    activityRate = 1.55;
                    break;
                case ActivityLevel.Heavy:
                    activityRate = 1.725;
                    break;
                case ActivityLevel.Athlete:
                    activityRate = 1.9;
                    break;
                default:
                    activityRate = 0.0;
                    break;

            }
            double bmrResult;
            if (tdee.Gender == Gender.Male)
            {
                bmrResult = 66 + (13.7 * tdee.Weight) +(5 * tdee.Height) - (6.8 * tdee.Age);
            }
            else
            {
                bmrResult = 655 + (9.6 * tdee.Weight) +(1.8 * tdee.Height) - (4.7 * tdee.Age);
            }

            var tdeeResult = 0.0;
            tdeeResult += bmrResult * activityRate;
            ViewBag.Result = Convert.ToInt32(tdeeResult);

            return View("TDEECalculator");
        }


    }
}