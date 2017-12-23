using FitnessTech.Data;
using FitnessTech.Data.Entities;
using FitnessTech.Repositories.Interfaces;

namespace FitnessTech.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>,IOrderItemRepository
    {
        private readonly FitnessContext _context;
        public OrderItemRepository(FitnessContext context) : base(context)
        {
            _context = context;
        }
    }
}
