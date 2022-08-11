using FlatRockTechnology.eCommerce.Core.Contracts.Patterns;
using FlatRockTechnology.eCommerce.Core.Contracts.Repositories;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Models.Category;
using FlatRockTechnology.eCommerce.Core.Entities;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			this.categoryRepository = categoryRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<CategoryModel> GetByIdAsync(Guid id)
		{
			var category = await categoryRepository.GetByIdAsync(id);

			if (category == null)
			{
				throw new ItemNotFoundException();
			}

			return new CategoryModel
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				IsActive = category.IsActive
			};
		}

		public async Task<IEnumerable<CategoryModel>> GetAllAsync()
		{
			var categories = await categoryRepository.GetAllAsync();

			if (!categories.Any())
			{
				throw new EmptyCollectionException();
			}

			return categories.Select(c => new CategoryModel
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				IsActive = c.IsActive
			})
			.ToList();
		}

		public async Task<CategoryModel> CreateAsync(CategoryModel categoryModel)
		{
			var category = await categoryRepository.GetByIdAsync(categoryModel.Id);

			if (category != null)
			{
				throw new ItemAlreadyExistException();
			}

			category = new CategoryEntity
			{
				Name = categoryModel.Name
			};

			await categoryRepository.CreateAsync(category);
			await unitOfWork.CompleteAsync();

			return categoryModel;
		}

		public async Task<CategoryModel> EditAsync(CategoryModel categoryModel)
		{
			var category = await categoryRepository.GetByIdAsync(categoryModel.Id);

			if (category == null)
			{
				throw new ItemNotFoundException();
			}

			category.Id = categoryModel.Id;
			category.Name = categoryModel.Name;
			category.Description = categoryModel.Description;

			categoryRepository.Edit(category);
			await unitOfWork.CompleteAsync();

			return categoryModel;
		}

		public async Task DeleteByIdAsync(Guid id)
		{
			var category = await categoryRepository.GetByIdAsync(id);

			if (category == null || !category.IsActive)
			{
				throw new ItemNotFoundException();
			}

			category.IsActive = false;
			await unitOfWork.CompleteAsync();
		}
	}
}