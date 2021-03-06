﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;
using FitnessTech.Data.Helpers;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Repositories
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        List<Exercise> GetByMuscleGroup(MuscleGroup muscleGroup);

    }
}
