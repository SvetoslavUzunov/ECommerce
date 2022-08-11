using Moq;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Service.Services;
using FlatRockTechnology.eCommerce.Core.Models.Product;
using FlatRockTechnology.eCommerce.Core.Exceptions;

namespace FlatRockTechnology.eCommerce.Test.Services
{
	[TestClass]
	public class ProductServiceTest
	{
		private IProductService productService;
		private readonly Mock<IProductRepository> productRepositoryMock = new();
		private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
		private ProductEntity productEntity;

		[TestInitialize]
		public void Setup()
		{
			productService = new ProductService(productRepositoryMock.Object, unitOfWorkMock.Object);
			productEntity = new ProductEntity();
		}

		[TestMethod]
		public async Task GetByIdMethodShoutReturnProduct()
		{
			productEntity = CreateProductEntity();

			productRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(productEntity);

			var product = await productService.GetByIdAsync(productEntity.Id);

			Assert.AreEqual(productEntity.Id, product.Id);
		}

		[TestMethod]
		public async Task GetByIdMethodShoutThrowException()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await productService.GetByIdAsync(GetProductId()));

		[TestMethod]
		public async Task GetAllMethodShoutReturnProducts()
		{
			const int CountProducts = 5;

			var products = new HashSet<ProductEntity>();

			for (int i = 0; i < CountProducts; i++)
			{
				productEntity = CreateProductEntity();

				products.Add(productEntity);
			}

			productRepositoryMock
				.Setup(x => x.GetAllAsync())
				.ReturnsAsync(products);

			var allProducts = await productService.GetAllAsync();

			Assert.AreEqual(products.Count, allProducts.Count());
		}

		[TestMethod]
		public async Task GetAllMethodShoutThrowException()
			=> await Assert.ThrowsExceptionAsync<EmptyCollectionException>(async () => await productService.GetAllAsync());

		[TestMethod]
		public async Task CreateMethodShoutCreateProduct()
		{
			productEntity = CreateProductEntity();

			productRepositoryMock
				.Setup(x => x.CreateAsync(productEntity));

			var productModel = CreateProductModel();
			var createdProduct = await productService.CreateAsync(productModel);

			Assert.AreEqual(productEntity.Name, createdProduct.Name);
		}

		[TestMethod]
		public async Task CreateMethodShoutThrowExceptionWhenProductAlreadyExist()
		{
			productEntity = CreateProductEntity();

			productRepositoryMock
				.Setup(x => x.CreateAsync(It.IsAny<ProductEntity>()));

			productRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(productEntity);

			await Assert.ThrowsExceptionAsync<ItemAlreadyExistException>(async () => await productService.CreateAsync(CreateProductModel()));
		}

		[TestMethod]
		public async Task EditMethodShoutEditProduct()
		{
			const string EditProductName = "IChangedYourName";

			productEntity = CreateProductEntity();

			productRepositoryMock
				.Setup(x => x.Edit(It.IsAny<ProductEntity>()));

			productRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(productEntity);

			productEntity.Name = EditProductName;
			var productModel = CreateProductModel();

			var editedProduct = await productService.EditAsync(productModel);

			Assert.AreEqual(productEntity.Name, editedProduct.Name);
		}

		[TestMethod]
		public async Task EditMethodShoutThrowExceptionWhenProductIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await productService.EditAsync(CreateProductModel()));

		[TestMethod]
		public async Task DeleteByIdMethodShoutDeleteProduct()
		{
			productEntity = CreateProductEntity();

			productRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(productEntity);

			await productService.DeleteByIdAsync(productEntity.Id);

			unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
		}

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenProductIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await productService.DeleteByIdAsync(GetProductId()));

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenProductAlreadyIsDeleted()
		{
			var productModel = CreateProductModel();
			productModel.IsActive = false;

			await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await productService.DeleteByIdAsync(productModel.Id));
		}

		private ProductModel CreateProductModel()
		{
			return new ProductModel
			{
				Id = productEntity.Id,
				Name = productEntity.Name
			};
		}

		private ProductEntity CreateProductEntity()
		{
			productEntity.Id = GetProductId();
			productEntity.Name = GetProductName();

			return productEntity;
		}

		private static Guid GetProductId()
			=> Guid.NewGuid();

		private static string GetProductName()
			=> $"Product: {new Random().Next(10)}";
	}
}
