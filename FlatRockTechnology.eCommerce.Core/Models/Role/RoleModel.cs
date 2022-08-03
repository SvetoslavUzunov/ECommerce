using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.User;

namespace FlatRockTechnology.eCommerce.Core.Models.Role
{
	public class RoleModel : BaseModel
	{
		[Required]
		[StringLength(RoleConstants.NameMaxLength, MinimumLength = RoleConstants.NameMinLength)]
		public string Name { get; set; }

		public virtual ICollection<UserModel>? Users { get; set; } = new HashSet<UserModel>();
	}
}