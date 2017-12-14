using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.Data
{
    public interface IFitnessRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
   

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);


        bool SaveAll();

        void AddEntity(object model);
    }
}