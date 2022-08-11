using Microsoft.AspNetCore.Mvc;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Contracts.Services;
using FlatRockTechnology.eCommerce.Core.Models.User;
using FlatRockTechnology.eCommerce.Core.Models.Token;
using Microsoft.AspNetCore.Authorization;

namespace FlatRockTechnology.eCommerce.Api.Controllers
{
	[Route(WebConstants.ControllerRoute)]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
			=> this.authenticationService = authenticationService;

		//[Authorize(Roles = RoleConstants.ClientRole)]
		[HttpPost(WebConstants.ActionRoute)]
		public Task Register([FromBody] UserRegistrationModel registrationModel)
			=> authenticationService.RegisterAsync(registrationModel);

		//[Authorize(Roles = RoleConstants.ClientRole)]
		[HttpPost(WebConstants.ActionRoute)]
		public Task<TokenModel> Login([FromBody] UserLoginModel loginModel)
			=> authenticationService.LoginAsync(loginModel);

		[HttpPost(WebConstants.ActionRoute)]
		public Task<TokenModel> RefreshToken([FromBody] string refreshToken)
			=> authenticationService.RefreshTokenAsync(refreshToken);
	}
}