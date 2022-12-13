using CityInfo.API;
using CityInfo.API.DbContexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders(); // Initially nothing to be logged
//builder.Logging.AddConsole(); // Add console to log

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers(options =>// Only AddControllers service registered as we will be building an API, which returns JSON responses
{
    options.ReturnHttpNotAcceptable = true; // Handle when user requests a response format that is not supported. RETURNS 406 Not Acceptable
}).AddXmlDataContractSerializerFormatters().AddNewtonsoftJson();

// Transient lifetime services are created each time they're requested -- used in lightweight/stateless services
// Scoped lifetime services are created once per request
// Singleton lifetime services are created the first time they're requested, with each subsequent request using the same instance

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); // Allows us to inject a FileExtensionContentTypeProvider anywhere in our code

// Bespoke services
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>(); // Whenever we inject IMailService, we want to provide it an instance of LocalMailService
#else 
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"], // Only accept tokens from this authority as valid, aka created by our API
            ValidAudience = builder.Configuration["Authentication:Audience"], // Checks if the token meant for the API
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeFromLondon", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city", "London");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Order matters for middleware,
// before checking for endpoints and authorization,
// we want to check if the request is authenicated at all
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
