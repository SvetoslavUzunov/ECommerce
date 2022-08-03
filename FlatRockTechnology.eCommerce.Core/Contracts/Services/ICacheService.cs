using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Core.Contracts.Services
{
	public interface ICacheService
	{
		public T GetData<T>(string key);

		public void SetData<T>(string key, T value, DateTimeOffset expirationTime);

		public void RemoveData(string key);

		public DateTimeOffset AddTime(double minutes = CacheConstants.ExpirationTime);
	}
}
