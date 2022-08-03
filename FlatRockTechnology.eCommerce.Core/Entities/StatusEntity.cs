using System.ComponentModel.DataAnnotations;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class StatusEntity
	{
		public StatusEntity()
		{
			this.Orders = new HashSet<OrderEntity>();
		}
		
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(StatusConstants.NameMaxLength, MinimumLength = StatusConstants.NameMinLength)]
		public string Name { get; set; }

		public virtual ICollection<OrderEntity>? Orders { get; set; }
	}
}