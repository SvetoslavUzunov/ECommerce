using Microsoft.AspNetCore.Mvc;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.User;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;

namespace FlatRockTechnology.eCommerce.Api.Controllers
{
	[Route(WebConstants.ControllerRoute)]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService userService;

		public UsersController(IUserService userService)
			=> this.userService = userService;

		[HttpGet(WebConstants.ActionRouteId)]
		public async Task<UserModel> GetById(Guid id)
			=> await userService.GetByIdAsync(id);

		[HttpGet(WebConstants.ActionRoute)]
		public async Task<IEnumerable<UserModel>> GetAll()
			=> await userService.GetAllAsync();

		[HttpPost(WebConstants.ActionRoute)]
		public async Task<UserModel> Create([FromBody] UserModel user)
			=> await userService.CreateAsync(user);

		[HttpPut(WebConstants.ActionRoute)]
		public async Task<UserModel> Edit([FromBody] UserModel user)
			=> await userService.EditAsync(user);

		[HttpDelete(WebConstants.ActionRouteId)]
		public async Task DeleteById(Guid id)
			=> await userService.DeleteByIdAsync(id);
	}
}