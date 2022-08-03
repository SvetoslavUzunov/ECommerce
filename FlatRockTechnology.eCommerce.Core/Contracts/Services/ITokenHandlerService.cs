using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Models.Token;

namespace FlatRockTechnology.eCommerce.Core.Contracts.Services
{
	public interface ITokenHandlerService
	{
		public Task<TokenModel> GenerateToken(UserEntity user);

		public Task<IList<string>> GetUserRoles(UserEntity user);

		public string ValidateRefreshToken(string refreshToken);
	}
}
