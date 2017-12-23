using FitnessTech.Data;
using FitnessTech.Services;
using FitnessTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        //injecting services
        public HomeController(IMailService mailService)
        {
            _mailService = mailService;
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
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
