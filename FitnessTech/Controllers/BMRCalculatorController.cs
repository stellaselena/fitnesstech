using AutoMapper;
using FitnessTech.Business_Logic;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FitnessTech.Controllers
{
    public class BMRCalculatorController : Controller
    {
        private readonly IMapper _mapper;

        public BMRCalculatorController(IMapper mapper)
        {
            _mapper = mapper;

        }

        public IActionResult Index()
        {
            return View("BMRCalculator");
        }

        [HttpPost]
        public IActionResult Index(BMRViewModel model)
        {

            if (!ModelState.IsValid) return View("BMRCalculator");
            var bmr = _mapper.Map<BMRViewModel, BMR>(model);
            var bmrResult = Calculator.BmrCalculator(bmr);
            ViewBag.Result = Convert.ToInt32(bmrResult);

            return View("BMRCalculator");
        }


    }
}
