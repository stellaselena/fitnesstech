using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;

namespace FitnessTech.Data
{
    public class FitnessRepository : IFitnessRepository
    {
        private readonly FitnessContext _context;

        public FitnessRepository(FitnessContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.OrderBy(p => p.Title).ToList();
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
