using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FlatRockTechnology.eCommerce.Core.Constants;
using FlatRockTechnology.eCommerce.Core.Entities;
using FlatRockTechnology.eCommerce.Core.Provider;
using FlatRockTechnology.eCommerce.DataLayer;

namespace FlatRockTechnology.eCommerce.Api.Configurations
{
	public static class BuilderConfigurations
	{
		public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
		{
			AddTokenConfigurations(services, configuration);
			AddAuthenticationRequirements(services);
			AddDatabase(services, configuration);
		}

		private static IServiceCollection AddTokenConfigurations(IServiceCollection services, IConfiguration configuration)
		{
			var tokenKey = new TokenSettings(configuration).KeyAsString;
			var keyAsBytes = Encoding.UTF8.GetBytes(tokenKey);

			var issue = new TokenSettings(configuration).Issue;
			var audience = new TokenSettings(configuration).Audience;

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(keyAsBytes),
					ValidateIssuer = true,
					ValidIssuer = issue,
					ValidateAudience = true,
					ValidAudience = audience,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(TokenConstants.Policy, policy =>
				{
					policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
					policy.RequireAuthenticatedUser();
				});
			});

			return services;
		}

		private static IServiceCollection AddDatabase(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = new DBConnectionString(configuration).GetDbConnectionString;

			return services.AddDbContext<ECommerceDBContext>(options => options.UseSqlServer(connectionString));
		}

		private static IdentityBuilder AddAuthenticationRequirements(IServiceCollection services)
			=> services.AddIdentity<UserEntity, RoleEntity>(
					options =>
					{
						options.Password.RequiredLength = UserConstants.PasswordMinLength;
						options.Password.RequireUppercase = true;
						options.Password.RequireLowercase = true;
						options.Password.RequireDigit = true;
						options.Password.RequireNonAlphanumeric = true;
					})
					.AddRoles<RoleEntity>()
					.AddEntityFrameworkStores<ECommerceDBContext>();
	}
}
