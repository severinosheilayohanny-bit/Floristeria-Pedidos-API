using Floristeria.Domain.Entities;

namespace Floristeria.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);
    }
}
