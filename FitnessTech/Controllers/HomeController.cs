using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using Microsoft.AspNetCore.Mvc;
using FitnessTech.Models;
using FitnessTech.Services;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FitnessTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IFitnessRepository _repository;
        //injecting services
        public HomeController(IMailService mailService, IFitnessRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("Stella Selena", "azaela@gmail.com", "This is a test message.");
                ViewBag.Message = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                
            }
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            return View(results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
