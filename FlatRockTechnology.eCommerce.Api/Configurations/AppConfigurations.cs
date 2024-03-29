﻿using Microsoft.AspNetCore.Diagnostics;
using FlatRockTechnology.eCommerce.Core.Models.Errors;
using static System.Net.Mime.MediaTypeNames;

namespace FlatRockTechnology.eCommerce.Api.Configurations
{
	public static class AppConfigurations
	{
		public static void AddConfigurations(this IApplicationBuilder app)
			=> ConfigureExceptionHandler(app);

		private static void ConfigureExceptionHandler(IApplicationBuilder app)
		{
			app.UseExceptionHandler(exceptionHandlerApp =>
			{
				exceptionHandlerApp.Run(async context =>
				{
					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					context.Response.ContentType = Text.Plain;

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

					var errorModel = new ErrorModel()
					{
						Errors = new List<string> { contextFeature.Error.Message }
					};

					if (contextFeature != null)
					{
						await context.Response.WriteAsJsonAsync(string.Join(Environment.NewLine, errorModel.Errors));
					}

					if (contextFeature?.Path == "/")
					{
						await context.Response.WriteAsJsonAsync(errorModel);
					}
				});
			});
		}
	}
}
