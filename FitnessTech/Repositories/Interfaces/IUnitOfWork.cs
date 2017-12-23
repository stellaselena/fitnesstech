using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; set; }
        IEmployeeRepository EmployeeRepository { get; set; }
        IGymRepository GymRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IOrderItemRepository OrderItemRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IExerciseRepository ExerciseRepository { get; set; }
        IWorkoutTypeRepository WorkoutTypeRepository { get; set; }
        IWorkoutRepository WorkoutRepository { get; set; }
        IWorkoutProgramRepository WorkoutProgramRepository { get; set; }

        void Complete();

    }

}
