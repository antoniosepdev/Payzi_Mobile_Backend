using Microsoft.EntityFrameworkCore;
using Payzi.Mobile.Api.Startup.FluentValidation;
using Payzi.Mobile.Api.Startup.IEndpoint;
using Payzi.Mobile.Api.Startup.Swagger;
 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionStrings = builder.Configuration.GetConnectionString("Payzi");

string? secret = builder.Configuration.GetValue<string>("Secret");

Payzi.Abstraction.StaticParams.StaticParams.Secret = secret;

builder.Services.AddSingleton(connectionStrings);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});


builder.Services.AddDbContext<Payzi.Context.Context>(x => x.UseSqlServer(connectionStrings, x => x.EnableRetryOnFailure()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

//app.MapControllers();

app.Run();
