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
    public class GymRepository : Repository<Gym>, IGymRepository
    {
        private readonly FitnessContext _context;
        public GymRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Gym> GetGymWithInclude(int? id)
        {
            return await _context.Gyms.Include(g => g.Employees).Include(g => g.Customers)
                .SingleOrDefaultAsync(m => m.GymId == id);
        }
    }
}
