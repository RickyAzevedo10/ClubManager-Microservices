using MassTransit;
using Microsoft.EntityFrameworkCore;
using TrainingCompetition.API.Configuration;
using TrainingCompetition.API.Midlewares;
using TrainingCompetition.Application.Consumers;
using TrainingCompetition.Application.Interfaces.Infrastructure;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infrastructure.Persistence.CachedRepositories;
using TrainingCompetition.Ioc;
using TrainingCompetition.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMappingConfiguration();

builder.Services.AddAuthorizationSettings(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application dependencies
builder.Services.AddApplicationDependencies(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

// Add Redis Cache
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration.GetConnectionString("Redis")!;
    redisOptions.Configuration = connection;
});

builder.Services.AddScoped<IUserPermissionsCachedRepository, UserPermissionsCachedRepository>();

builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();

    configure.AddConsumer<TeamCreateUpdateConsumer>();
    configure.AddConsumer<TeamDeleteConsumer>();
    configure.AddConsumer<PlayerCreateUpdateConsumer>();
    configure.AddConsumer<PlayerDeleteConsumer>();

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]!), h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// Builders
WebApplication? app = builder.Build();

//Garantir que a base de dados seja criada e as migrations aplicadas
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate(); // Cria a base de dados e aplica as migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseMiddleware<RequestURLMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorizationSettings();
app.MapControllers();

app.Run();
