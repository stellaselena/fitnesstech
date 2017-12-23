using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTech.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IUnitOfWork unitOfWork, ILogger<ProductsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_unitOfWork.ProductRepository.GetAllProducts()) ;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get products: {e}");
                return BadRequest("Failed to get products");
            }
        }
    }
}
