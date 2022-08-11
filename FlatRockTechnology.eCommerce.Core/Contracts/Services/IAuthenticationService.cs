using FlatRockTechnology.eCommerce.Core.Models.Token;
using FlatRockTechnology.eCommerce.Core.Models.User;

namespace FlatRockTechnology.eCommerce.Core.Contracts.Services
{
	public interface IAuthenticationService
	{
		public Task RegisterAsync(UserRegistrationModel userModel);

		public Task<TokenModel> LoginAsync(UserLoginModel userModel);

		public Task<TokenModel> RefreshTokenAsync(string refreshToken);
	}
}