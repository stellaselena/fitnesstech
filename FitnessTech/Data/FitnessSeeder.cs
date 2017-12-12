using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace FitnessTech.Data
{
    public class FitnessSeeder
    {
        private readonly FitnessContext _context;
        private readonly IHostingEnvironment _hosting;

        public FitnessSeeder(FitnessContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                //create sample data from json
                var file = Path.Combine(_hosting.ContentRootPath, "Data/supplement.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
