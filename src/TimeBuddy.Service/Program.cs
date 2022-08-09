using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TimeBuddy.Service.Configurations;
using TimeBuddy.Service.Contexts;
using TimeBuddy.Service.GraphQL;

var builder = WebApplication.CreateBuilder(args);

var postgresConfiguration = PostgresConfiguration.ReadFromConfiguration(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var authSection = builder.Configuration.GetSection("Authentication");
        opt.Authority = authSection.GetValue<string>("Authority");
    });
builder.Services.AddAuthorization();

builder.Services
    .AddCors(opt => opt.AddDefaultPolicy(policyBuilder =>
    {
        var origins = builder.Configuration.GetValue<string>("Origins");
        policyBuilder.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    }))
    .AddPooledDbContextFactory<TimeBuddyContext>(opt => opt
        .UseNpgsql(postgresConfiguration.GetConnectionString()))
    .AddGraphQLServer()
    .AddAuthorization()
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var dbContext = app.Services
    .GetRequiredService<IDbContextFactory<TimeBuddyContext>>()
    .CreateDbContext();
dbContext.Database.Migrate();

app.UseCors();
app.MapGraphQL();

app.Run();