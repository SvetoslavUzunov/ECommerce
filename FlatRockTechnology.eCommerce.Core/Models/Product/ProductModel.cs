using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Models.Product
{
	public class ProductModel : BaseModel
	{
		[Required]
		[StringLength(ProductConstants.NameMaxLength, MinimumLength = ProductConstants.NameMinLength)]
		public string Name { get; set; }

		[StringLength(ProductConstants.DescriptionMaxLength, MinimumLength = ProductConstants.DescriptionMinLength)]
		public string? Description { get; set; }

		[Range(ProductConstants.PriceMinValue, ProductConstants.PriceMaxValue)]
		public decimal? Price { get; set; }

		public string? ImageUrl { get; set; }
	}
}