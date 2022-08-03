using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public abstract class BaseEntity
	{
		public BaseEntity()
		{
			this.Id = Guid.NewGuid();
			this.IsActive = true;
			this.CreatedAt = DateTime.Now;
		}

		[Key]
		public Guid Id { get; set; }

		public bool IsActive { get; set; }

		public Guid? CreatedById { get; set; }

		public DateTime CreatedAt { get; set; }

		public Guid? ModifiedById { get; set; }

		public DateTime? ModifiedAt { get; set; }

		[ForeignKey(nameof(CreatedById))]
		public virtual UserEntity? CreatedBy { get; set; }

		[ForeignKey(nameof(ModifiedById))]
		public virtual UserEntity? ModifiedBy { get; set; }
	}
}
