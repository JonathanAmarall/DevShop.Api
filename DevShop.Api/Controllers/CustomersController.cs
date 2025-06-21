using Microsoft.AspNetCore.Mvc;
using DevShop.Api.Models;
using DevShop.Api.Views;
using DevShop.Api.Repositories;
using AutoMapper;

namespace DevShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerView>>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerView>>(customers);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerView>> GetCustomer(string id)
        {
            var customer = await _customerRepository.GetWithOrdersAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerView>(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerView>> CreateCustomer(CreateCustomerView createCustomerView)
        {
            var customer = _mapper.Map<Customer>(createCustomerView);
            
            var createdCustomer = await _customerRepository.AddAsync(customer);
            var customerView = _mapper.Map<CustomerView>(createdCustomer);
            
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, customerView);
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, UpdateCustomerView updateCustomerView)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCustomerView, customer);
            await _customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
} 