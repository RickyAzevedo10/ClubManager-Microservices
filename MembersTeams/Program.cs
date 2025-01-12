using MassTransit;
using MembersTeams.API.Configuration;
using MembersTeams.API.Midlewares;
using MembersTeams.Application.Consumers;
using MembersTeams.Application.Interfaces.Infrastructure;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infrastructure.Persistence.CachedRepositories;
using MembersTeams.Infrastructure.Services;
using MembersTeams.Ioc;
using MembersTeams.Middlewares;
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

    configure.AddConsumer<InstitutionCreateUpdateConsumer>();
    configure.AddConsumer<InstitutionDeleteConsumer>();
    configure.AddConsumer<UserCreateUpdateConsumer>();
    configure.AddConsumer<UserDeleteConsumer>();

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
    var fakeService = scope.ServiceProvider.GetRequiredService<FakerService>();

    dbContext.Database.Migrate(); // Cria a base de dados e aplica as migrations

    // Realiza o seed dos dados
    await SeedData.SeedAsync(dbContext, fakeService);
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
