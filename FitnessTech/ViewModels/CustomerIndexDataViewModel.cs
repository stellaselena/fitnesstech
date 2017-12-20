using FitnessTech.Data.Entities;
using System.Collections.Generic;

namespace FitnessTech.ViewModels
{
    public class CustomerIndexDataViewModel
    {
        public CustomerIndexDataViewModel()
        {
            Customers = new List<Customer>();
        }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
