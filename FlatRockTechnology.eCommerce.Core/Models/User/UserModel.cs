using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Models.Role;

namespace FlatRockTechnology.eCommerce.Core.Models.User
{
	public class UserModel : BaseModel
	{
		[StringLength(UserConstants.UserNameMaxLength, MinimumLength = UserConstants.UserNameMinLength)]
		public string? UserName { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(UserConstants.PhoneNumberMaxLength, MinimumLength = UserConstants.PhoneNumberMinLength)]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(UserConstants.FirstNameMaxLength, MinimumLength = UserConstants.FirstNameMinLength)]
		public string? FirstName { get; set; }

		[Required]
		[StringLength(UserConstants.LastNameMaxLength, MinimumLength = UserConstants.LastNameMinLength)]
		public string? LastName { get; set; }

		[Url]
		public string? AvatarUrl { get; set; }

		public int RoleId { get; set; }

		public virtual RoleModel Role { get; set; }
	}
}