using System;
using System.Collections.Generic;
using System.Linq;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly FitnessContext _context;
        public ProductRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return Queryable.OrderBy<Product, string>(_context.Products, p => p.Title).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return Queryable.Where<Product>(_context.Products, p => p.Category == category).OrderBy(p => p.Title).ToList();
        }

       
    }
}
