using Microsoft.AspNetCore.Mvc;
using DevShop.Api.Models;
using DevShop.Api.Views;
using DevShop.Api.Repositories;
using AutoMapper;

namespace DevShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllWithDetailsAsync();
            return _mapper.Map<List<OrderView>>(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderView>> GetOrder(string id)
        {
            var order = await _orderRepository.GetWithDetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderView>(order);
        }

        // GET: api/orders/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrdersByCustomer(string customerId)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<List<OrderView>>(orders);
        }

        // GET: api/orders/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrdersByStatus(OrderStatus status)
        {
            var orders = await _orderRepository.GetByStatusAsync(status);
            return _mapper.Map<List<OrderView>>(orders);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<OrderView>> CreateOrder(CreateOrderView createOrderView)
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetByIdAsync(createOrderView.CustomerId);
            if (customer == null)
            {
                return BadRequest("Cliente não encontrado");
            }

            var order = _mapper.Map<Order>(createOrderView);
            
            foreach (var itemView in createOrderView.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemView.ProductId);
                if (product == null)
                {
                    return BadRequest($"Produto com ID {itemView.ProductId} não encontrado");
                }

                var orderItem = _mapper.Map<OrderItem>(itemView, opt => opt.Items["OrderId"] = order.Id);
                order.AddItem(orderItem);
            }

            var createdOrder = await _orderRepository.AddAsync(order);

            var orderWithDetails = await _orderRepository.GetWithDetailsAsync(createdOrder.Id);
            var orderView = _mapper.Map<OrderView>(orderWithDetails);
            
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, orderView);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, UpdateOrderView updateOrderView)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(updateOrderView, order);
            await _orderRepository.UpdateAsync(order);

            return NoContent();
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(order);

            return NoContent();
        }
    }
} 