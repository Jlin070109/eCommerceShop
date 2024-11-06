using eCommerceShop.Entities;
namespace eCommerceShop.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Order> AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}
