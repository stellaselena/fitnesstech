using FitnessTech.Data.Entities;
using System.Collections.Generic;

namespace FitnessTech.ViewModels
{
    public class EmployeeIndexDataViewModel
    {

        public EmployeeIndexDataViewModel()
        {
            Employees = new List<Employee>();
        }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
