using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;
using Floristeria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Floristeria.Infrastructure.Models;
using Floristeria.Infrastructure.Interfaces;

namespace Floristeria.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository, IOrderSummaryRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<OrderSummaryModel>> GetSummariesAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderSummaryModel
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Total = o.Total
                })
                .ToListAsync();
        }


        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            return await _context.Customers
                .AnyAsync(c => c.Id == customerId);
        }

        public async Task<Dictionary<int, Product>> GetProductsByIdsAsync(
            List<int> productIds)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .FindAsync(order.Id);

            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.Status = order.Status;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return false;
            }

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
