using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Data.Helpers;
using FitnessTech.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private readonly FitnessContext _context;

        public ExerciseRepository(FitnessContext context) : base(context)
        {
            _context = context;


        }

        public List<Exercise> GetByMuscleGroup(MuscleGroup muscleGroup)
        {
            return  _context.Exercises.Where(e => e.MuscleGroup == muscleGroup).ToList();
        }
    }
}
