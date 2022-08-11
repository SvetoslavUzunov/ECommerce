using Moq;
using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Models.Category;
using FlatRockTechnology.eCommerce.Service.Services;
using FlatRockTechnology.eCommerce.Core.Exceptions;

namespace FlatRockTechnology.eCommerce.Test.Services
{
	[TestClass]
	public class CategoryServiceTest
	{
		private ICategoryService categoryService;
		private readonly Mock<ICategoryRepository> categoryRepositoryMock = new();
		private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
		private CategoryEntity categoryEntity;

		[TestInitialize]
		public void Setup()
		{
			categoryService = new CategoryService(categoryRepositoryMock.Object, unitOfWorkMock.Object);
			categoryEntity = new CategoryEntity();
		}

		[TestMethod]
		public async Task GetByIdMethodShoutReturnCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			var category = await categoryService.GetByIdAsync(categoryEntity.Id);

			Assert.AreEqual(categoryEntity.Id, category.Id);
		}

		[TestMethod]
		public async Task GetByIdMethodShoutThrowException()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await categoryService.GetByIdAsync(GetCategoryId()));

		[TestMethod]
		public async Task GetAllMethodShoutReturnCategories()
		{
			const int CountCategories = 5;

			var categories = new HashSet<CategoryEntity>();

			for (int i = 0; i < CountCategories; i++)
			{
				categoryEntity = CreateCategoryEntity();

				categories.Add(categoryEntity);
			}

			categoryRepositoryMock
				.Setup(x => x.GetAllAsync())
				.ReturnsAsync(categories);

			var allCategories = await categoryService.GetAllAsync();

			Assert.AreEqual(categories.Count, allCategories.Count());
		}

		[TestMethod]
		public async Task GetAllMethodShoutReturnException()
			=> await Assert.ThrowsExceptionAsync<EmptyCollectionException>(async () => await categoryService.GetAllAsync());

		[TestMethod]
		public async Task CreateMethodShoutCreateCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.CreateAsync(categoryEntity));

			var categoryModel = CreateCategoryModel();

			var createdCategory = await categoryService.CreateAsync(categoryModel);

			Assert.AreEqual(categoryEntity.Name, createdCategory.Name);
		}

		[TestMethod]
		public async Task CreateMethodShoutThrowExceptionWhenCategoryAlreadyExist()
		{
			var categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.CreateAsync(It.IsAny<CategoryEntity>()));

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			await Assert.ThrowsExceptionAsync<ItemAlreadyExistException>(async () => await categoryService.CreateAsync(CreateCategoryModel()));
		}

		[TestMethod]
		public async Task EditMethodShoutCreateCategory()
		{
			const string EditCategoryName = "IChangedYourName";

			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.Edit(categoryEntity));

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			categoryEntity.Name = EditCategoryName;
			var categoryModel = CreateCategoryModel();

			var editedCategory = await categoryService.EditAsync(categoryModel);

			Assert.AreEqual(categoryEntity.Name, editedCategory.Name);
		}

		[TestMethod]
		public async Task EditMethodShoutThrowExceptionWhenCategoryIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await categoryService.EditAsync(CreateCategoryModel()));

		[TestMethod]
		public async Task DeleteByIdMethodShoutDeleteCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			await categoryService.DeleteByIdAsync(categoryEntity.Id);

			unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
		}

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenCategoryIsNull()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await categoryService.DeleteByIdAsync(GetCategoryId()));

		[TestMethod]
		public async Task DeleteMethodShoutThrowExceptionWhenCategoryAlreadyIsDeleted()
		{
			var categoryModel = CreateCategoryModel();
			categoryModel.IsActive = false;

			await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await categoryService.DeleteByIdAsync(categoryModel.Id));
		}

		private CategoryModel CreateCategoryModel()
		{
			return new CategoryModel
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name
			};
		}

		private CategoryEntity CreateCategoryEntity()
		{
			categoryEntity.Id = GetCategoryId();
			categoryEntity.Name = GetCategoryName();

			return categoryEntity;
		}

		private static Guid GetCategoryId()
			=> Guid.NewGuid();

		private static string GetCategoryName()
			=> $"Category: {new Random().Next(10)}";
	}
}
