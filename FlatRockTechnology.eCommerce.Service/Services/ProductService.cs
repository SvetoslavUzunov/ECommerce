using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Models.Product;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository productRepository;
		private readonly IUnitOfWork unitOfWork;

		public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
		{
			this.productRepository = productRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<ProductModel> GetByIdAsync(Guid id)
		{
			var product = await productRepository.GetByIdAsync(id);

			if (product == null)
			{
				throw new ItemNotFoundException();
			}

			return new ProductModel
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				IsActive = product.IsActive
			};
		}

		public async Task<IEnumerable<ProductModel>> GetAllAsync()
		{
			var products = await productRepository.GetAllAsync();

			if (!products.Any())
			{
				throw new EmptyCollectionException();
			}

			return products.Select(c => new ProductModel
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				IsActive = c.IsActive
			})
			.ToList();
		}

		public async Task<ProductModel> CreateAsync(ProductModel productModel)
		{
			var product = await productRepository.GetByIdAsync(productModel.Id);

			if (product != null)
			{
				throw new ItemAlreadyExistException();
			}

			product = new ProductEntity
			{
				Name = productModel.Name,
				Price = productModel.Price,
				Description = productModel.Description
			};

			await productRepository.CreateAsync(product);
			await unitOfWork.CompleteAsync();

			return productModel;
		}

		public async Task<ProductModel> EditAsync(ProductModel productModel)
		{
			var product = await productRepository.GetByIdAsync(productModel.Id);

			if (product == null)
			{
				throw new ItemNotFoundException();
			}

			product.Id = productModel.Id;
			product.Name = productModel.Name;
			product.Price = productModel.Price;
			product.Description = productModel.Description;

			productRepository.Edit(product);
			await unitOfWork.CompleteAsync();

			return productModel;
		}

		public async Task DeleteByIdAsync(Guid id)
		{
			var product = await productRepository.GetByIdAsync(id);

			if (product == null || !product.IsActive)
			{
				throw new ItemNotFoundException();
			}

			product.IsActive = false;
			await unitOfWork.CompleteAsync();
		}
	}
}