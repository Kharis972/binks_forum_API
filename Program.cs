using binks_forum_API.Data;
using Microsoft.EntityFrameworkCore;
using binks_forum_API.Repositories.Interfaces;
using binks_forum_API.Repositories;
using binks_forum_API.Service.Interfaces;
using binks_forum_API.Service;

// Création du WebApplicationBuilder pour configurer l'application.
var builder = WebApplication.CreateBuilder(args);

// Ajout des contrôleurs.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Binks Forum API", Version = "v1" });
    }
);

builder.Logging.AddConsole();

builder.Services.AddOpenApi();

// Configuration des Repositories et Services (génériques et spécifiques à l'entité)
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
                .AddScoped<IUserRepository, UserRepository>()

                .AddScoped(typeof(IService<,>), typeof(Service<,>))
                .AddScoped<IUserService, UserService>();

// Configuration de l'URL de la base de données venant de appsettings.json
builder.Services.AddDbContext<ApplicationDataBaseContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("DefaultConnection")!);
    },ServiceLifetime.Scoped
);

var app = builder.Build();

// Configuration du pipeline HTTP de l'application.
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Binks Forum API v1");
        c.RoutePrefix = string.Empty; // Si tu veux accéder à Swagger sans /swagger
    }
);
app.UseRouting();
app.MapControllers();

// Lance l'application Web.
app.Run();