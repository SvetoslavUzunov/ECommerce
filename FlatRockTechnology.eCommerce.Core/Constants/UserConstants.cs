namespace FlatRockTechnology.eCommerce.Core.Constants
{
	public static class UserConstants
	{
		public const int UserNameMinLength = 1;
		public const int UserNameMaxLength = 30;
		public const int PhoneNumberMinLength = 10;
		public const int PhoneNumberMaxLength = 15;
		public const int FirstNameMinLength = 1;
		public const int FirstNameMaxLength = 30;
		public const int LastNameMinLength = 1;
		public const int LastNameMaxLength = 30;
		public const int PasswordMinLength = 7;

		public const string AdminUserName = "AdminUser";
		public const string AdminUserEmail = "admin@user.com";
		public const string AdminUserPassword = "Pass123#";

		public const string UserTokenKey = "userToken";

		public const string UserAlreadyExist = "User already exist!";
		public const string UserNotFound = "User not found!";
	}
}