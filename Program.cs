using System.Reflection;
using binks_forum_API.Data;
using Microsoft.EntityFrameworkCore;
using binks_forum_API.Repositories;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Services;
using binks_forum_API.Services.Interfaces;
using binks_forum_API.Service;
using binks_forum_API.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Binks Forum API", Version = "v1" }));
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

var jwtKey = builder.Configuration["Jwt:SigningKey"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    if (builder.Environment.IsProduction())
        throw new InvalidOperationException("JWT signing key is missing. Set Jwt__SigningKey (at least 32 characters).");
    jwtKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
}
if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
    throw new InvalidOperationException("Jwt__SigningKey must contain at least 32 bytes.");
builder.Configuration["Jwt:SigningKey"] = jwtKey;

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "BinksForumAPI",
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "BinksForumWeb",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = System.Security.Claims.ClaimTypes.Name,
            RoleClaimType = System.Security.Claims.ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("auth", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueLimit = 0;
        limiter.AutoReplenishment = true;
    });
});
builder.Services.AddSingleton<binks_forum_API.Services.TokenService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));

// Register every repository and service interface implemented by this assembly.
// This keeps the controllers' complete dependency graph available at runtime.
var applicationTypes = Assembly.GetExecutingAssembly().GetTypes();
foreach (var implementation in applicationTypes.Where(t => t is { IsClass: true, IsAbstract: false }))
{
    if (implementation.IsGenericTypeDefinition) continue;
    var isRepository = implementation.Namespace == typeof(Repository<,>).Namespace;
    var isService = implementation.Namespace is "binks_forum_API.Service" or "binks_forum_API.Services";
    if (!isRepository && !isService) continue;

    foreach (var serviceContract in implementation.GetInterfaces())
    {
        if (serviceContract == typeof(IDisposable)) continue;
        builder.Services.AddScoped(serviceContract, implementation);
    }
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseConfigured = !string.IsNullOrWhiteSpace(connectionString);
if (!databaseConfigured)
{
    if (builder.Environment.IsProduction())
        throw new InvalidOperationException("Database connection is missing. Set ConnectionStrings__DefaultConnection before deployment.");
    // Lets the web app and its local preview start before the production database is supplied.
    connectionString = "Server=127.0.0.1;Port=3306;Database=binks_forum;User ID=root;Password=;";
}

builder.Services.AddDbContext<ApplicationDataBaseContext>(options => options.UseMySQL(connectionString!));
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("database", tags: new[] { "ready" });

var app = builder.Build();

app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Binks Forum API v1");
        options.RoutePrefix = "swagger";
    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.Use(async (context, next) =>
{
    var requestedUserId = context.Request.Query["userId"].FirstOrDefault()
        ?? context.Request.Query["adminId"].FirstOrDefault();
    if (!string.IsNullOrWhiteSpace(requestedUserId))
    {
        var authenticatedUserId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (authenticatedUserId is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        if (!string.Equals(authenticatedUserId, requestedUserId, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }
    }
    await next();
});
app.MapControllers();
app.MapFallback("/api/{**path}", () => Results.NotFound());
app.MapFallbackToFile("index.html");

app.Run();

public sealed class DatabaseHealthCheck(ApplicationDataBaseContext database) : Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheck
{
    public async Task<Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult> CheckHealthAsync(
        Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await database.Database.CanConnectAsync(cancellationToken)
                ? Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy()
                : Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy("Database is unreachable.");
        }
        catch (Exception exception)
        {
            return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy("Database is unreachable.", exception);
        }
    }
}
