using Floristeria.Application.Contract;
using Floristeria.Application.Core;
using Floristeria.Application.Dtos.Customer;
using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;

namespace Floristeria.Application.Service
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            return await _customerRepository.AddAsync(customer);
        }

        public async Task<bool> UpdateAsync(
            int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return false;
            }

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            return await _customerRepository.UpdateAsync(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}
