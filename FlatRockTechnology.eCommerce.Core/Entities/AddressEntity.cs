using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class AddressEntity
	{
		public AddressEntity()
		{
			this.Id = Guid.NewGuid();
			this.UserAddresses = new HashSet<UserAddressesEntity>();
			this.Orders = new HashSet<OrderEntity>();
		}

		public Guid Id { get; set; }

		[Required]
		[StringLength(AddressConstants.NameMaxLength, MinimumLength = AddressConstants.NameMinLength)]
		public string Name { get; set; }

		public virtual ICollection<UserAddressesEntity>? UserAddresses { get; set; }

		public virtual ICollection<OrderEntity>? Orders { get; set; }
	}
}