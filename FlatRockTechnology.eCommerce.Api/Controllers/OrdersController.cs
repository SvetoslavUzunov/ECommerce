using Microsoft.AspNetCore.Mvc;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.Order;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;

namespace FlatRockTechnology.eCommerce.Api.Controllers
{
	[Route(WebConstants.ControllerRoute)]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService orderService;

		public OrdersController(IOrderService orderService)
			=> this.orderService = orderService;

		[HttpGet(WebConstants.ActionRouteId)]
		public async Task<OrderModel> GetByid(Guid id)
			=> await orderService.GetByIdAsync(id);

		[HttpGet(WebConstants.ActionRoute)]
		public async Task<IEnumerable<OrderModel>> GetAll()
			=> await orderService.GetAllAsync();

		[HttpPost(WebConstants.ActionRoute)]
		public async Task<OrderModel> Create([FromBody] OrderModel order)
			=> await orderService.CreateAsync(order);

		[HttpPut(WebConstants.ActionRoute)]
		public async Task<OrderModel> Edit([FromBody] OrderModel order)
			=> await orderService.EditAsync(order);

		[HttpDelete(WebConstants.ActionRouteId)]
		public async Task DeleteById(Guid id)
			=> await orderService.DeleteByIdAsync(id);
	}
}