namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class OrderEntity : BaseEntity
	{
		public OrderEntity()
		{
			this.OrderProducts = new HashSet<OrderProductsEntity>();
		}

		public int? Quantity { get; set; }

		public decimal? TotalPrice { get; set; }

		public int? StatusId { get; set; }

		public virtual StatusEntity? Status { get; set; }

		public int? DeliveryAddressId { get; set; }

		public virtual AddressEntity? DeliveryAddress { get; set; }

		public int? UserId { get; set; }

		public virtual UserEntity? User { get; set; }

		public virtual ICollection<OrderProductsEntity>? OrderProducts { get; set; }
	}
}