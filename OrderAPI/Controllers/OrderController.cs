using Microsoft.AspNetCore.Mvc;
using OrderApplicationCore.Interfaces;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationCore.Entities.Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ApplicationCore.Entities.Order>>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationCore.Entities.Order>> CreateOrder(ApplicationCore.Entities.Order order)
        {
            var createdOrder = await _orderRepository.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrdersByCustomerId), new { customerId = order.CustomerId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, ApplicationCore.Entities.Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}