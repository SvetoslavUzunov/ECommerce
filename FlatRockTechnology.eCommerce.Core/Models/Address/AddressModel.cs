using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Models.Address
{
	public class AddressModel : BaseModel
	{
		[Required]
		[StringLength(AddressConstants.NameMaxLength, MinimumLength = AddressConstants.NameMinLength)]
		public string Name { get; set; }
	}
}