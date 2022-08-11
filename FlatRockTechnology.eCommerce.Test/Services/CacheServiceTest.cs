using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Models.Cache;
using FlatRockTechnology.eCommerce.Service.Services;

namespace FlatRockTechnology.eCommerce.Test.Services
{
	[TestClass]
	public class CacheServiceTest
	{
		private ICacheService cacheService;
		private CacheModel cacheData;
		private const int ExpirationTime = 10;

		[TestInitialize]
		public void Setup()
		{
			cacheService = new CacheService();
			cacheData = new CacheModel();
		}

		[TestMethod]
		public void SetMethodShoutSetValue()
		{
			var cacheData = CreateCache();

			try
			{
				cacheService.SetData(cacheData.Key, cacheData.Value, DateTimeOffset.Now.AddMinutes(ExpirationTime));
			}
			catch (Exception)
			{
				throw;
			}
		}

		[TestMethod]
		public void GetMethodShoutReturnCorrectValue()
		{
			cacheData = CreateCache();

			cacheService.SetData(cacheData.Key, cacheData.Value, DateTimeOffset.Now.AddMinutes(ExpirationTime));

			var getValue = cacheService.GetData<int>(cacheData.Key);

			Assert.AreEqual(cacheData.Value, getValue);
		}

		[TestMethod]
		public void RemoveMethodShoutRemoveValueFromCache()
		{
			cacheData = CreateCache();

			cacheService.SetData(cacheData.Key, cacheData.Value, DateTimeOffset.Now.AddMinutes(ExpirationTime));

			try
			{
				cacheService.RemoveData(cacheData.Key);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private CacheModel CreateCache()
		{
			cacheData.Key = GetKey();
			cacheData.Value = GetValue();

			return cacheData;
		}

		private static string GetKey()
			=> new Random().Next(100).ToString();

		private static int GetValue()
			=> new Random().Next(100);
	}
}
