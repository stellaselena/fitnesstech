using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitnessContext _context;
        public ICustomerRepository CustomerRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IGymRepository GymRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IOrderItemRepository OrderItemRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IExerciseRepository ExerciseRepository { get; set; }
        public IWorkoutTypeRepository WorkoutTypeRepository { get; set; }
        public IWorkoutRepository WorkoutRepository { get; set; }
        public IWorkoutProgramRepository WorkoutProgramRepository { get; set; }

        public UnitOfWork(FitnessContext context)
        {
            _context = context;
            CustomerRepository = new CustomerRepository(_context);
            EmployeeRepository = new EmployeeRepository(context);
            GymRepository = new GymRepository(context);
            OrderRepository = new OrderRepository(context) ;
            OrderItemRepository = new OrderItemRepository(context);
            ProductRepository = new ProductRepository(context);
            ExerciseRepository = new ExerciseRepository(context);
            WorkoutTypeRepository = new WorkoutTypeRepository(context);
            WorkoutRepository = new WorkoutRepository(context);
            WorkoutProgramRepository = new WorkoutProgramRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();

        }

        public void CompleteAsync()
        {
                _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
