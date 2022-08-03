namespace FlatRockTechnology.eCommerce.Core.Entities
{
	public class ProductCategoriesEntity
	{
		public ProductCategoriesEntity()
		{
			this.ProductId = Guid.NewGuid();
			this.CategoryId = Guid.NewGuid();
		}

		public Guid ProductId { get; set; }

		public virtual ProductEntity Product { get; set; }

		public Guid CategoryId { get; set; }

		public virtual CategoryEntity Category { get; set; }
	}
}