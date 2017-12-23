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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly FitnessContext _context;
        public CustomerRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }
    }
    
}
