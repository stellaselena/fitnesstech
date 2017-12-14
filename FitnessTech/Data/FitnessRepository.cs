using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTech.Data
{
    public class FitnessRepository : IFitnessRepository
    {
        private readonly FitnessContext _context;
        private readonly ILogger<FitnessRepository> _logger;

        public FitnessRepository(FitnessContext context, ILogger<FitnessRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all: {e}");
                return null;
            }            
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).OrderBy(p => p.Title).ToList();
        }


        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Orders
                    .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                    .OrderBy(p => p.OrderDate).ToList();
            }
            else
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Orders
                    .OrderBy(p => p.OrderDate).ToList();
            }
           

        }
        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(p => p.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.Id == id)
                .OrderBy(p => p.OrderDate).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }
    }
}
