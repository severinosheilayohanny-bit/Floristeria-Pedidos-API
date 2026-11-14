using Floristeria.Infrastructure.Models;

namespace Floristeria.Infrastructure.Interfaces
{
    public interface IOrderSummaryRepository
    {
        Task<List<OrderSummaryModel>> GetSummariesAsync();
    }
}
