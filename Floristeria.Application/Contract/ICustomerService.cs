using Floristeria.Application.Dtos.Customer;
using Floristeria.Domain.Entities;

namespace Floristeria.Application.Contract
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task<Customer> CreateAsync(CreateCustomerDto dto);

        Task<bool> UpdateAsync(int id, UpdateCustomerDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
