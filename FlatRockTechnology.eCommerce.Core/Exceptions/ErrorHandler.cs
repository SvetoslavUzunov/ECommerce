using Microsoft.AspNetCore.Identity;
using FlatRockTechnology.eCommerce.Core.Models.Errors;

namespace FlatRockTechnology.eCommerce.Core.Exceptions
{
	public static class ErrorHandler
	{
		public static void ExecuteErrorHandler(IdentityResult identityResult)
		{
			var errorModel = new ErrorModel();
			
			errorModel.Errors.AddRange(identityResult.Errors.Select(x => x.Description));

			throw new ValidationException(errorModel.Errors);
		}
	}
}
