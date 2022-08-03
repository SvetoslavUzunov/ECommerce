using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Models.Status
{
	public class StatusModel : BaseModel
	{
		[Required]
		[StringLength(StatusConstants.NameMaxLength, MinimumLength = StatusConstants.NameMinLength)]
		public string Name { get; set; }
	}
}