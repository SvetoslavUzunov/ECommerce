namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class UserRolesEntity
	{
		public UserRolesEntity()
		{
			this.UserId = Guid.NewGuid();
			this.RoleId = Guid.NewGuid();
		}

		public Guid UserId { get; set; }

		public virtual UserEntity User { get; set; }

		public Guid RoleId { get; set; }

		public virtual RoleEntity Role { get; set; }
	}
}