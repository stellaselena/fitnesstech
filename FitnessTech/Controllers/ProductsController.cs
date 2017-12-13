using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTech.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController
    {
        private readonly IFitnessRepository _repository;
        private readonly ILogger _logger;

        public ProductsController(IFitnessRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetAll();
        }
    }
}
