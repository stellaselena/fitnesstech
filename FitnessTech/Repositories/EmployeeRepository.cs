using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly FitnessContext _context;
        public EmployeeRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }

      
    }
}
