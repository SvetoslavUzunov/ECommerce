using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Models.Token;
using FlatRockTechnology.eCommerce.Core.Provider;
using FlatRockTechnology.eCommerce.Core.Constants;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class TokenHandlerService : ITokenHandlerService
	{
		private readonly UserManager<UserEntity> userManager;
		private readonly IConfiguration configuration;
		private readonly ICacheService cacheService;

		public TokenHandlerService(UserManager<UserEntity> userManager,
			IConfiguration configuration,
			ICacheService cacheService)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.cacheService = cacheService;
		}

		public async Task<TokenModel> GenerateToken(UserEntity user)
		{
			var tokenModel = new TokenModel()
			{
				AccessToken = await GenerateAccessToken(user),
				RefreshToken = GenerateRefreshToken()
			};

			cacheService.SetData(tokenModel.RefreshToken, user.Id, cacheService.AddTime());

			return tokenModel;
		}

		public string ValidateRefreshToken(string refreshToken)
		{
			var userId = cacheService.GetData<Guid>(refreshToken).ToString();

			if (userId == null)
			{
				throw new ValidationException();
			}
			else
			{
				cacheService.RemoveData(refreshToken);
			}

			return userId;
		}

		public async Task<IList<string>> GetUserRoles(UserEntity user)
			=> await userManager.GetRolesAsync(user);

		private async Task<string> GenerateAccessToken(UserEntity user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var userRoles = await GetUserRoles(user);

			foreach (var userRole in userRoles)
			{
				claims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			var tokenKey = new TokenSettings(configuration).KeyAsEncoding;
			var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

			var tokenOptions = new JwtSecurityToken
			(
				claims: claims,
				expires: DateTime.Now.AddDays(TokenConstants.CountDays),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		private static string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			using var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}
	}
}
