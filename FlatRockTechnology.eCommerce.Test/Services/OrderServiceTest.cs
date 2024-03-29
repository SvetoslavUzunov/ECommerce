﻿using Moq;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Models.Order;
using FlatRockTechnology.eCommerce.Service.Services;
using FlatRockTechnology.eCommerce.Core.Exceptions;

namespace FlatRockTechnology.eCommerce.Test.Services
{
	[TestClass]
	public class OrderServiceTest
	{
		private IOrderService orderService;
		private readonly Mock<IOrderRepository> orderRepositoryMock = new();
		private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
		private OrderEntity orderEntity;

		[TestInitialize]
		public void Setup()
		{
			orderService = new OrderService(orderRepositoryMock.Object, unitOfWorkMock.Object);
			orderEntity = new OrderEntity();
		}

		[TestMethod]
		public async Task GetByIdMethodShoutReturnOrder()
		{
			orderEntity = CreateOrderEntity();

			orderRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(orderEntity);

			var order = await orderService.GetByIdAsync(orderEntity.Id);

			Assert.AreEqual(orderEntity.Id, order.Id);
		}

		[TestMethod]
		public async Task GetByIdMethodShoutThrowException()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await orderService.GetByIdAsync(GetOrderId()));

		[TestMethod]
		public async Task GetAllMethodShoutReturnOrders()
		{
			const int CountOrders = 5;

			var orders = new HashSet<OrderEntity>();

			for (int i = 0; i < CountOrders; i++)
			{
				orderEntity = CreateOrderEntity();

				orders.Add(orderEntity);
			}

			orderRepositoryMock
				.Setup(x => x.GetAllAsync())
				.ReturnsAsync(orders);

			var allOrders = await orderService.GetAllAsync();

			Assert.AreEqual(orders.Count, allOrders.Count());
		}

		[TestMethod]
		public async Task GetAllMethodShoutReturnException()
			=> await Assert.ThrowsExceptionAsync<EmptyCollectionException>(async () => await orderService.GetAllAsync());

		[TestMethod]
		public async Task CreateMethodShoutCorrectCreateOrder()
		{
			orderEntity = CreateOrderEntity();

			orderRepositoryMock
				.Setup(x => x.CreateAsync(orderEntity));

			var orderModel = CreateOrderModel();

			var createdOrder = await orderService.CreateAsync(orderModel);

			Assert.AreEqual(orderEntity.Id, createdOrder.Id);
		}

		[TestMethod]
		public async Task CreateMethodShoutThrowExceptionWhenOrderAlreadyExist()
		{
			var orderEntity = CreateOrderEntity();

			orderRepositoryMock
				.Setup(x => x.CreateAsync(It.IsAny<OrderEntity>()));

			orderRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(orderEntity);

			await Assert.ThrowsExceptionAsync<ItemAlreadyExistException>(async () => await orderService.CreateAsync(CreateOrderModel()));
		}

		[TestMethod]
		public async Task EditMethodShoutEditOrder()
		{
			Guid editOrderId = GetOrderId();

			orderEntity = CreateOrderEntity();

			orderRepositoryMock
				.Setup(x => x.Edit(orderEntity));

			orderRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(orderEntity);

			orderEntity.Id = editOrderId;
			var orderModel = CreateOrderModel();

			var editedOrder = await orderService.EditAsync(orderModel);

			Assert.AreEqual(orderEntity.Id, editedOrder.Id);
		}

		[TestMethod]
		public async Task EditMethodShoutThrowExceptionWhenOrderIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await orderService.EditAsync(CreateOrderModel()));

		[TestMethod]
		public async Task DeleteByIdMethodShoutDeleteOrder()
		{
			orderEntity = CreateOrderEntity();

			orderRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(orderEntity);

			await orderService.DeleteByIdAsync(orderEntity.Id);

			unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
		}

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenOrderIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await orderService.DeleteByIdAsync(GetOrderId()));

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenOrderAlreadyIsDeleted()
		{
			var orderModel = CreateOrderModel();
			orderModel.IsActive = false;

			await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await orderService.DeleteByIdAsync(orderModel.Id));
		}

		private OrderModel CreateOrderModel()
		{
			return new OrderModel
			{
				Id = orderEntity.Id
			};
		}

		private OrderEntity CreateOrderEntity()
		{
			orderEntity.Id = GetOrderId();

			return orderEntity;
		}

		private static Guid GetOrderId()
			=> Guid.NewGuid();
	}
}
