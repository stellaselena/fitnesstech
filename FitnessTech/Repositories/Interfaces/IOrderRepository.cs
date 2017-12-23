using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);
        void AddOrder(Order newOrder);



    }
}
