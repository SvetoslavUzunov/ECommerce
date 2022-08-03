using Microsoft.AspNetCore.Mvc;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.Category;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;

namespace FlatRockTechnology.eCommerce.Api.Controllers
{
	[Route(WebConstants.ControllerRoute)]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoriesController(ICategoryService categoryService)
			=> this.categoryService = categoryService;

		[HttpGet(WebConstants.ActionRouteId)]
		public async Task<CategoryModel> GetById(Guid id)
			=> await categoryService.GetByIdAsync(id);

		[HttpGet(WebConstants.ActionRoute)]
		public async Task<IEnumerable<CategoryModel>> GetAll()
			=> await categoryService.GetAllAsync();

		[HttpPost(WebConstants.ActionRoute)]
		public async Task<CategoryModel> Create([FromBody] CategoryModel category)
			=> await categoryService.CreateAsync(category);

		[HttpPut(WebConstants.ActionRoute)]
		public async Task<CategoryModel> Edit([FromBody] CategoryModel category)
			=> await categoryService.EditAsync(category);

		[HttpDelete(WebConstants.ActionRouteId)]
		public async Task DeleteById(Guid id)
			=> await categoryService.DeleteByIdAsync(id);
	}
}