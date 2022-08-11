using Microsoft.AspNetCore.Identity;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Exceptions;
using FlatRockTechnology.eCommerce.Core.Models.Token;
using FlatRockTechnology.eCommerce.Core.Models.User;

namespace FlatRockTechnology.eCommerce.Service.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<UserEntity> userManager;
		private readonly ITokenHandlerService tokenHandlerService;

		public AuthenticationService(UserManager<UserEntity> userManager,
			ITokenHandlerService tokenHandlerService)
		{
			this.userManager = userManager;
			this.tokenHandlerService = tokenHandlerService;
		}

		public async Task RegisterAsync(UserRegistrationModel registrationModel)
		{
			var user = new UserEntity
			{
				UserName = registrationModel.UserName,
				Email = registrationModel.Email
			};

			var isUserCreated = await userManager.CreateAsync(user, registrationModel.Password);

			if (!isUserCreated.Succeeded)
			{
				ErrorHandler.ExecuteErrorHandler(isUserCreated);
			}

			var isUserAddedToRole = await userManager.AddToRoleAsync(user, RoleConstants.AdminRole);

			if (!isUserAddedToRole.Succeeded)
			{
				ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
			}
		}

		public async Task<TokenModel> LoginAsync(UserLoginModel loginModel)
		{
			var user = await userManager.FindByNameAsync(loginModel.UserName);

			if (user == null)
			{
				throw new ValidationException(UserConstants.UserNotFound);
			}

			if (!await userManager.CheckPasswordAsync(user, loginModel.Password))
			{
				throw new ValidationException(ResponseConstants.UnauthorizedAccess);
			}

			return await tokenHandlerService.GenerateToken(user);
		}

		public async Task<TokenModel> RefreshTokenAsync(string refreshToken)
		{
			var userId = tokenHandlerService.ValidateRefreshToken(refreshToken);

			var user = await userManager.FindByIdAsync(userId);

			if (user == null)
			{
				throw new ItemNotFoundException();
			}

			return await tokenHandlerService.GenerateToken(user);
		}
	}
}