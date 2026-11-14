using Floristeria.Application.Contract;
using Floristeria.Application.Core;
using Floristeria.Application.Dtos.Order;
using Floristeria.Domain.Entities;
using Floristeria.Domain.Interfaces;

namespace Floristeria.Application.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<ServiceResult> CreateAsync(CreateOrderDto dto)
        {
            var customerExists =
                await _orderRepository.CustomerExistsAsync(dto.CustomerId);

            if (!customerExists)
            {
                return CreateErrorResult("El cliente no existe.");
            }

            if (dto.OrderDetails == null || dto.OrderDetails.Count == 0)
            {
                return CreateErrorResult(
                    "El pedido debe contener productos.");
            }

            if (dto.OrderDetails
                .GroupBy(detail => detail.ProductId)
                .Any(group => group.Count() > 1))
            {
                return CreateErrorResult(
                    "No repitas productos en el pedido.");
            }

            var productIds = dto.OrderDetails
                .Select(detail => detail.ProductId)
                .ToList();

            var products =
                await _orderRepository.GetProductsByIdsAsync(productIds);

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            foreach (var item in dto.OrderDetails)
            {
                if (!products.TryGetValue(
                    item.ProductId, out var product))
                {
                    return CreateErrorResult(
                        $"El producto {item.ProductId} no existe.");
                }

                if (!product.IsAvailable)
                {
                    return CreateErrorResult(
                        $"El producto {product.Name} no está disponible.");
                }

                if (product.Stock < item.Quantity)
                {
                    return CreateErrorResult(
                        $"Stock insuficiente para {product.Name}.");
                }

                var subtotal = product.Price * item.Quantity;

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = subtotal
                });

                order.Total += subtotal;
            }

            var createdOrder = await _orderRepository.AddAsync(order);

            return new ServiceResult
            {
                Success = true,
                Message = "Pedido creado correctamente.",
                Data = createdOrder
            };
        }

        public async Task<bool> UpdateAsync(
            int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return false;
            }

            order.Status = dto.Status;

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
