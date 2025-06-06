using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);  // Returns all customers
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();  // Return 404 if the customer is not found
            }
            return Ok(customer);  // Return the customer with the given ID
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto customerDto)
        {
            await _customerService.AddCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.Id }, customerDto);  // Return 201 Created with the customer
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest("Customer ID mismatch");  // Return 400 Bad Request if IDs don't match
            }

            await _customerService.UpdateCustomerAsync(customerDto);
            return NoContent();  // Return 204 No Content if the update is successful
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();  // Return 204 No Content if the deletion is successful
        }
    }
}
