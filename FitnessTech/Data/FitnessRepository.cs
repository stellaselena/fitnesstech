using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessTech.Data
{
    public class FitnessRepository : IFitnessRepository
    {
        //TODO: repository pattern and logging
        private readonly FitnessContext _context;
        private readonly ILogger<FitnessRepository> _logger;

        public FitnessRepository(FitnessContext context, ILogger<FitnessRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll was called");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all: {e}");
                return null;
            }
            
        }

        public IEnumerable<Product> GetByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).OrderBy(p => p.Title).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
