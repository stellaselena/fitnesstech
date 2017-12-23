using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        Task<Workout> GetThenInclude(int? id);
        Task<IEnumerable<Workout>> GetAllThenInclude();


    }
}
