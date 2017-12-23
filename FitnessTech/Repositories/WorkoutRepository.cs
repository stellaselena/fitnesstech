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
    public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
    {
        private readonly FitnessContext _context;
        public WorkoutRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Workout>> GetAllThenInclude()
        {
               return  await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.ExerciseAssigments)
                .ThenInclude(w => w.Exercise)
                .OrderBy(w => w.WorkoutName)
                .ToListAsync();
        }

        public async Task<Workout> GetThenInclude(int? id)
        {
            return await _context.Workouts
                .Include(w => w.WorkoutType)
                .Include(w => w.ExerciseAssigments)
                .ThenInclude(w => w.Exercise)
                .SingleAsync(w => w.WorkoutId == id);


        }
    }
}
