using AutoMapper;
using FitnessTech.Business_Logic;
using FitnessTech.Data.Entities;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;


namespace FitnessTech.Controllers
{
    public class TDEECalculatorController : Controller
    {
        private readonly IMapper _mapper;

        public TDEECalculatorController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View("TDEECalculator");
        }


        [HttpPost]
        public IActionResult Index(TDEEViewModel model)
        {
            if (!ModelState.IsValid) return View("TDEECalculator");
            var tdee = _mapper.Map<TDEEViewModel, TDEE>(model);
            var tdeeResult = Calculator.TdeeCalculator(tdee);
            ViewBag.Result = Convert.ToInt32(tdeeResult);

            return View("TDEECalculator");
        }
    }
}
