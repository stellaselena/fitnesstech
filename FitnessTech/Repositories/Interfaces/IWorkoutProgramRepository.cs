using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data.Entities;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IWorkoutProgramRepository : IRepository<WorkoutProgram>
    {
        Task<IEnumerable<WorkoutProgram>> GetAllThenInclude();
        Task<WorkoutProgram> GetThenInclude(int? id);

    }

}
