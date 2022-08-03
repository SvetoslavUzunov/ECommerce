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
		public Task Register([FromBody] UserRegistrationModel userModel)
			=> authenticationService.RegisterAsync(userModel);

		//[Authorize(Roles = RoleConstants.ClientRole)]
		[HttpPost(WebConstants.ActionRoute)]
		public Task<TokenModel> Login([FromBody] UserLoginModel userModel)
			=> authenticationService.LoginAsync(userModel);

		[HttpPost(WebConstants.ActionRoute)]
		public Task<TokenModel> RefreshToken([FromBody] string refreshToken)
			=> authenticationService.RefreshToken(refreshToken);
	}
}