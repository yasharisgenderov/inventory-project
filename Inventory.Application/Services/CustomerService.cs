using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Get all customers
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Contact = c.Contact
            });
        }

        // Get customer by ID
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Contact = customer.Contact
            };
        }

        // Add a new customer
        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                Contact = customerDto.Contact
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        // Update an existing customer
        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerDto.Id);
            if (customer == null) return;

            customer.Name = customerDto.Name;
            customer.Contact = customerDto.Contact;

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        // Delete customer by ID
        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
