namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class OrderProductsEntity
	{
		public OrderProductsEntity()
		{
			this.OrderId = Guid.NewGuid();
			this.ProductId = Guid.NewGuid();
		}

		public Guid OrderId { get; set; }

		public virtual OrderEntity Order { get; set; }

		public Guid ProductId { get; set; }

		public virtual ProductEntity Product { get; set; }
	}
}