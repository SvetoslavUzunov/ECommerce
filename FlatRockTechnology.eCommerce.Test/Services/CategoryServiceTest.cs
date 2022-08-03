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
		public async Task GetByIdMethodShoutReturnCorrectCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			var category = await categoryService.GetByIdAsync(categoryEntity.Id);

			Assert.AreEqual(categoryEntity.Id, category.Id);
		}

		[TestMethod]
		public async Task GetByIdMethodShoutThrowCorrectException()
			=> await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await categoryService.GetByIdAsync(GetCategoryId()));

		[TestMethod]
		public async Task GetAllMethodShoutReturnCorrectCategories()
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
		public async Task GetAllMethodShoutReturnCorrectException()
			=> await Assert.ThrowsExceptionAsync<EmptyCollectionException>(async () => await categoryService.GetAllAsync());

		[TestMethod]
		public async Task CreateMethodShoutCorrectCreateCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.CreateAsync(categoryEntity));

			var categoryModel = CreateCategoryModel();

			var createdCategory = await categoryService.CreateAsync(categoryModel);

			Assert.AreEqual(categoryEntity.Name, createdCategory.Name);
		}

		[TestMethod]
		public async Task EditMethodShoutCorrectCreateCategory()
		{
			const string EditCategoryName = "IChangedYourName";

			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.Edit(categoryEntity));

			categoryEntity.Name = EditCategoryName;
			var categoryModel = CreateCategoryModel();

			var editedCategory = await categoryService.EditAsync(categoryModel);

			Assert.AreEqual(categoryEntity.Name, editedCategory.Name);
		}

		[TestMethod]
		public async Task DeleteByIdMethodShoutCorrectDeleteCategory()
		{
			categoryEntity = CreateCategoryEntity();

			categoryRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(categoryEntity);

			await categoryService.DeleteByIdAsync(categoryEntity.Id);

			unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
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
