using FlatRockTechnology.eCommerce.Core.Models.Address;
using FlatRockTechnology.eCommerce.Core.Models.Status;
using FlatRockTechnology.eCommerce.Core.Models.User;

namespace FlatRockTechnology.eCommerce.Core.Models.Order
{
	public class OrderModel : BaseModel
	{
		public int Quantity { get; set; }

		public decimal TotalPrice { get; set; }

		public int StatusId { get; set; }

		public virtual StatusModel Status { get; set; }

		public int DeliveryAddressId { get; set; }

		public virtual AddressModel DeliveryAddress { get; set; }

		public int UserId { get; set; }

		public virtual UserModel User { get; set; }
	}
}