using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;
using Floristeria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Floristeria.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries
                .Include(d => d.Order)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Delivery?> GetByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.Order)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> OrderExistsAsync(int orderId)
        {
            return await _context.Orders
                .AnyAsync(o => o.Id == orderId);
        }

        public async Task<bool> DeliveryExistsForOrderAsync(int orderId)
        {
            return await _context.Deliveries
                .AnyAsync(d => d.OrderId == orderId);
        }

        public async Task<Delivery> AddAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);

            await _context.SaveChangesAsync();

            return delivery;
        }

        public async Task<bool> UpdateAsync(Delivery delivery)
        {
            var existingDelivery = await _context.Deliveries
                .FindAsync(delivery.Id);

            if (existingDelivery == null)
            {
                return false;
            }

            existingDelivery.Status = delivery.Status;
            existingDelivery.DeliveredDate = delivery.DeliveredDate;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return false;
            }

            _context.Deliveries.Remove(delivery);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
