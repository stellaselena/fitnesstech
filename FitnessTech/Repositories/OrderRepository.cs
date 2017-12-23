using System;
using System.Collections.Generic;
using System.Linq;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly FitnessContext _context;

        public OrderRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return EntityFrameworkQueryableExtensions.Include<Order, ICollection<OrderItem>>(_context.Orders, p => p.Items)
                    .ThenInclude(p => p.Product)
                    .OrderBy(p => p.OrderDate).ToList();
            }
            else
            {
                return Queryable.OrderBy<Order, DateTime>(_context.Orders, p => p.OrderDate).ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return Queryable.Where<Order>(_context.Orders, o => o.User.UserName == username)
                    .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                    .OrderBy(p => p.OrderDate).ToList();
            }
            else
            {
                return Queryable.Where<Order>(_context.Orders, o => o.User.UserName == username)
                    .OrderBy(p => p.OrderDate).ToList();
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return Queryable.Where<Order>(_context.Orders, o => o.User.UserName == username)
                .Include(p => p.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.Id == id)
                .OrderBy(p => p.OrderDate).FirstOrDefault();
        }

        public void AddOrder(Order newOrder)
        {
            //convert new products to lookup of product
            foreach (var item in newOrder.Items)
            {
                item.Product = _context.Products.Find(item.Product.Id);
            }
            AddEntity(newOrder);
        }
    }
}
