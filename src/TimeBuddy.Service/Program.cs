using Microsoft.EntityFrameworkCore;
using TimeBuddy.Service.Configurations;
using TimeBuddy.Service.Contexts;
using TimeBuddy.Service.GraphQL;

var builder = WebApplication.CreateBuilder(args);

var postgresConfiguration = PostgresConfiguration.ReadFromConfiguration(builder.Configuration);

builder.Services
    .AddCors(opt => opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5001")
            .AllowAnyHeader()
            .AllowAnyMethod();
    }))
    .AddPooledDbContextFactory<TimeBuddyContext>(opt => opt
        .UseNpgsql(postgresConfiguration.GetConnectionString()))
    .AddGraphQLServer()
    .AddQueryableCursorPagingProvider()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .AddType<ProjectSettingsType>()
    .AddType<ProjectType>()
    .AddType<TimeFrameType>()
    .RegisterDbContext<TimeBuddyContext>(DbContextKind.Pooled)
    .AddMutationConventions()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

var dbContext = app.Services
    .GetRequiredService<IDbContextFactory<TimeBuddyContext>>()
    .CreateDbContext();
dbContext.Database.Migrate();

app.UseCors();
app.MapGraphQL();

app.Run();