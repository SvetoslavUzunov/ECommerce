using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class ProductEntity : BaseEntity
	{
		public ProductEntity()
		{
			this.OrderProducts = new HashSet<OrderProductsEntity>();
			this.ProductCategories = new HashSet<ProductCategoriesEntity>();
		}

		[Required]
		[StringLength(ProductConstants.NameMaxLength, MinimumLength = ProductConstants.NameMinLength)]
		public string Name { get; set; }

		[StringLength(ProductConstants.DescriptionMaxLength, MinimumLength = ProductConstants.DescriptionMinLength)]
		public string? Description { get; set; }

		[Range(ProductConstants.PriceMinValue, ProductConstants.PriceMaxValue)]
		public decimal? Price { get; set; }

		[Url]
		public string? ImageUrl { get; set; }

		public virtual ICollection<OrderProductsEntity>? OrderProducts { get; set; }

		public virtual ICollection<ProductCategoriesEntity>? ProductCategories { get; set; }
	}
}