using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(FitnessContext context) : base(context)
        {
        }
    }
}
