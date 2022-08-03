using System.Runtime.Caching;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class CacheService : ICacheService
	{
		private readonly ObjectCache memoryCache = MemoryCache.Default;

		public void SetData<T>(string key, T value, DateTimeOffset expirationTime)
		{
			try
			{
				if (!string.IsNullOrEmpty(key))
				{
					memoryCache.Set(key, value, expirationTime);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public T GetData<T>(string key)
		{
			try
			{
				return (T)memoryCache.Get(key);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void RemoveData(string key)
		{
			try
			{
				if (!string.IsNullOrEmpty(key))
				{
					memoryCache.Remove(key);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DateTimeOffset AddTime(double minutes = CacheConstants.ExpirationTime)
			=> DateTimeOffset.Now.AddMinutes(minutes);
	}
}
