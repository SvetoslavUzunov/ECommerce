using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class CategoryEntity : BaseEntity
	{
		public CategoryEntity()
		{
			this.ParentCategoryId = Guid.NewGuid();
			this.ParentCategories = new HashSet<CategoryEntity>();
			this.ProductCategories = new HashSet<ProductCategoriesEntity>();
		}

		[Required]
		[StringLength(CategoryConstants.NameMaxLength, MinimumLength = CategoryConstants.NameMinLength)]
		public string Name { get; set; }

		[StringLength(CategoryConstants.DescriptionMaxLength, MinimumLength = CategoryConstants.DescriptionMinLength)]
		public string? Description { get; set; }

		public Guid? ParentCategoryId { get; set; }

		[ForeignKey(nameof(ParentCategoryId))]
		public virtual CategoryEntity? ParentCategory { get; set; }

		public virtual ICollection<CategoryEntity>? ParentCategories { get; set; }

		public virtual ICollection<ProductCategoriesEntity>? ProductCategories { get; set; }
	}
}