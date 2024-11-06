using eCommerceShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Interfaces;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<ApplicationCore.Entities.Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<ApplicationCore.Entities.Order> AddOrderAsync(ApplicationCore.Entities.Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderAsync(ApplicationCore.Entities.Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}