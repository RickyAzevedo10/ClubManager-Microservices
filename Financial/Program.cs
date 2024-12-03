using Financial.API.Configuration;
using Financial.API.Midlewares;
using Financial.Application.Consumers;
using Financial.Application.Interfaces.Infrastructure;
using Financial.Infra.Contexts;
using Financial.Infrastructure.Persistence.CachedRepositories;
using Financial.Ioc;
using Financial.Middlewares;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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

//add caching repositories
builder.Services.AddScoped<IUserPermissionsCachedRepository, UserPermissionsCachedRepository>();

builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();

    configure.AddConsumer<UserCreateUpdateConsumer>();
    configure.AddConsumer<UserDeleteConsumer>();
    configure.AddConsumer<ClubMemberCreateUpdateConsumer>();
    configure.AddConsumer<ClubMemberDeleteConsumer>();
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

// Garantir que a base de dados seja criada e as migrations aplicadas
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
