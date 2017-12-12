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

namespace FitnessTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly FitnessContext _context;
        //injecting services
        public HomeController(IMailService mailService, FitnessContext context)
        {
            _mailService = mailService;
            _context = context;
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

        public IActionResult Shop()
        {
            var results = _context.Products.OrderBy(p => p.Category).ToList();
            return View(results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
