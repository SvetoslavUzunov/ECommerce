namespace FlatRockTechnology.eCommerce.Core.Models
{
	public class BaseModel
	{
		public BaseModel()
		{
			this.Id = Guid.NewGuid();
			this.IsActive = true;
		}

		public Guid Id { get; set; }

		public bool IsActive { get; set; }
	}
}
