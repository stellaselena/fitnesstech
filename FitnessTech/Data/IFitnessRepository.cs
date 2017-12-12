using System.Collections.Generic;
using FitnessTech.Data.Entities;

namespace FitnessTech.Data
{
    public interface IFitnessRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategory(string category);
        bool SaveAll();
    }
}