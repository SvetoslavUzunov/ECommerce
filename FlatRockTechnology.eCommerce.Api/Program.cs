using FlatRockTechnology.eCommerce.Api.Configurations;
using FlatRockTechnology.eCommerce.DataLayer;
using FlatRockTechnology.eCommerce.Service;
using static System.Net.Mime.MediaTypeNames;
using static FlatRockTechnology.eCommerce.Service.ServiceInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddConfigurations();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseStatusCodePages(Text.Plain, "Status Code Page: {0}");

await app.SeedDataAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
