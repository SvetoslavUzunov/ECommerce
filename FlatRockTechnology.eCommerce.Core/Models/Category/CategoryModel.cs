using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Models.Category
{
	public class CategoryModel : BaseModel
	{
		[Required]
		[StringLength(CategoryConstants.NameMaxLength, MinimumLength = CategoryConstants.NameMinLength)]
		public string Name { get; set; }

		[StringLength(CategoryConstants.DescriptionMaxLength, MinimumLength = CategoryConstants.DescriptionMinLength)]
		public string? Description { get; set; }

		public int ParentCategoryId { get; set; }

		public virtual CategoryModel ParentCategory { get; set; }

		public virtual ICollection<CategoryModel>? ParentCategories { get; set; } = new HashSet<CategoryModel>();
	}
}