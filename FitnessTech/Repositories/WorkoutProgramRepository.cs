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
    public class WorkoutProgramRepository : Repository<WorkoutProgram>, IWorkoutProgramRepository
    {
        private readonly FitnessContext _context;
        public WorkoutProgramRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutProgram>> GetAllThenInclude()
        {
            return await _context.WorkoutPrograms
                .Include(wp => wp.WorkoutAssigments)
                .ThenInclude(wp => wp.Workout)
                .OrderBy(w => w.WorkoutProgramName)
                .ToListAsync();
        }

        public async Task<WorkoutProgram> GetThenInclude(int? id)
        {
            return await _context.WorkoutPrograms.Include(w => w.WorkoutAssigments)
                .ThenInclude(w => w.Workout).ThenInclude(w => w.WorkoutType)
                .Include(w => w.WorkoutAssigments).ThenInclude(w => w.Workout)
                .ThenInclude(w => w.ExerciseAssigments)
                .ThenInclude(w => w.Exercise)
                .SingleOrDefaultAsync(m => m.WorkoutProgramId == id);
        }
    }

   
}

