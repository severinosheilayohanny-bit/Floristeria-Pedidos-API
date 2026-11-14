using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;
using Floristeria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Floristeria.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers
                .FindAsync(customer.Id);

            if (existingCustomer == null)
            {
                return false;
            }

            _context.Entry(existingCustomer)
                .CurrentValues.SetValues(customer);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
