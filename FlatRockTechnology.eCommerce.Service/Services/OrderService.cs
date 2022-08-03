using FlatRockTechnology.eCommerce.Core.Models.Order;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository orderRepository;
		private readonly IUnitOfWork unitOfWork;

		public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
		{
			this.orderRepository = orderRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<OrderModel> GetByIdAsync(Guid id)
		{
			var order = await orderRepository.GetByIdAsync(id);

			if (order == null)
			{
				throw new ItemNotFoundException();
			}

			return new OrderModel
			{
				Id = order.Id,
				IsActive = order.IsActive
			};
		}

		public async Task<IEnumerable<OrderModel>> GetAllAsync()
		{
			var orders = await orderRepository.GetAllAsync();

			if (!orders.Any())
			{
				throw new EmptyCollectionException();
			}

			return orders.Select(o => new OrderModel
			{
				Id = o.Id,
				IsActive = o.IsActive
			})
			.ToList();
		}

		public async Task<OrderModel> CreateAsync(OrderModel orderModel)
		{
			var order = new OrderEntity
			{
				Id = orderModel.Id
			};

			await orderRepository.CreateAsync(order);
			await unitOfWork.CompleteAsync();

			return orderModel;
		}

		public async Task<OrderModel> EditAsync(OrderModel orderModel)
		{
			var order = new OrderEntity
			{
				Id = orderModel.Id
			};

			orderRepository.Edit(order);
			await unitOfWork.CompleteAsync();

			return orderModel;
		}

		public async Task DeleteByIdAsync(Guid id)
		{
			var order = await orderRepository.GetByIdAsync(id);

			if (order == null || !order.IsActive)
			{
				throw new ItemNotFoundException();
			}

			order.IsActive = false;
			await unitOfWork.CompleteAsync();
		}
	}
}