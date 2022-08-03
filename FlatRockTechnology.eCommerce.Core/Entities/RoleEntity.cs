using Microsoft.AspNetCore.Identity;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class RoleEntity : IdentityRole<Guid>
	{
		public RoleEntity()
		{
			this.UserRoles = new HashSet<UserRolesEntity>();
		}

		public virtual ICollection<UserRolesEntity>? UserRoles { get; set; }
	}
}