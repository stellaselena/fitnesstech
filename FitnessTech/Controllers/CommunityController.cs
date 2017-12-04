using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTech.Controllers
{
    public class CommunityController : Controller
    {
        public IActionResult Index()
        {
            return View("CommunityIndex");
        }
    }
}