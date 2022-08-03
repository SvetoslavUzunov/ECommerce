using Microsoft.AspNetCore.Mvc;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.Product;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;

namespace FlatRockTechnology.eCommerce.Api.Controllers
{
	[Route(WebConstants.ControllerRoute)]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService productService;

		public ProductsController(IProductService productService)
			=> this.productService = productService;

		[HttpGet(WebConstants.ActionRouteId)]
		public async Task<ProductModel> GetById(Guid id)
			=> await productService.GetByIdAsync(id);

		[HttpGet(WebConstants.ActionRoute)]
		public async Task<IEnumerable<ProductModel>> GetAll()
			=> await productService.GetAllAsync();

		[HttpPost(WebConstants.ActionRoute)]
		public async Task<ProductModel> Create([FromBody] ProductModel product)
			=> await productService.CreateAsync(product);

		[HttpPut(WebConstants.ActionRoute)]
		public async Task<ProductModel> Edit([FromBody] ProductModel product)
			=> await productService.EditAsync(product);

		[HttpDelete(WebConstants.ActionRouteId)]
		public async Task DeleteById(Guid id)
			=> await productService.DeleteByIdAsync(id);
	}
}