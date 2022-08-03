using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class UserEntity : IdentityUser<Guid>
	{
		public UserEntity()
		{
			this.CreatedAt = DateTime.Now;
			this.IsActive = true;
			this.UserAddresses = new HashSet<UserAddressesEntity>();
			this.CreatedByCollection = new HashSet<UserEntity>();
			this.ModifiedByCollection = new HashSet<UserEntity>();
			this.Categories = new HashSet<CategoryEntity>();
			this.Roles = new HashSet<UserRolesEntity>();
			this.Orders = new HashSet<OrderEntity>();
		}

		[StringLength(UserConstants.FirstNameMaxLength, MinimumLength = UserConstants.FirstNameMinLength)]
		public string? FirstName { get; set; }

		[StringLength(UserConstants.LastNameMaxLength, MinimumLength = UserConstants.LastNameMinLength)]
		public string? LastName { get; set; }

		[Url]
		public string? AvatarUrl { get; set; }

		[EmailAddress]
		public override string Email { get; set; }

		public bool IsActive { get; set; }

		public Guid? CreatedById { get; set; }

		public DateTime CreatedAt { get; set; }

		public Guid? ModifiedById { get; set; }

		public DateTime? ModifiedAt { get; set; }

		[ForeignKey(nameof(CreatedById))]
		public virtual UserEntity? CreatedBy { get; set; }

		[ForeignKey(nameof(ModifiedById))]
		public virtual UserEntity? ModifiedBy { get; set; }

		public virtual ICollection<UserAddressesEntity>? UserAddresses { get; set; }

		public virtual ICollection<UserEntity>? CreatedByCollection { get; set; }

		public virtual ICollection<UserEntity>? ModifiedByCollection { get; set; }

		public virtual ICollection<OrderEntity>? Orders { get; set; }

		public virtual ICollection<CategoryEntity>? Categories { get; set; }

		public virtual ICollection<UserRolesEntity>? Roles { get; set; }
	}
}