using Mapster;
using Microsoft.EntityFrameworkCore;
using Payzi.Mobile.Api.Startup.FluentValidation;
using Payzi.Mobile.Api.Startup.IEndpoint;
using Payzi.Mobile.Api.Startup.Swagger;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

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

(Action<AuthenticationOptions> authenticationOptions, Action<JwtBearerOptions> jwtBearerOptions) = Payzi.Abstraction.Security.Security.AddAuthentication(secret);

builder.Services.AddAuthorization()
                .AddAuthentication(authenticationOptions)
                .AddJwtBearer(jwtBearerOptions);


builder.Services.AddDbContext<Payzi.Context.Context>(x => x.UseSqlServer(connectionStrings, x => x.EnableRetryOnFailure()));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1", new OpenApiInfo { Title = "Mi API", Version = "v1" }
        );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsLocalHost", //AllowSpecificOrigin
        builder => builder.WithOrigins(
            "https://localhost:7164",
            "https://localhost:5011",
            "https://localhost:3000",
            "") // Reemplaza con la URL de tu aplicación Flutter
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.ConfigureSwagger();
}

app.UseCors("AllowSpecificOrigin");

app.UseWebSockets();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

//app.MapControllers();

app.Run();
