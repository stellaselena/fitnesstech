using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

    }
}
