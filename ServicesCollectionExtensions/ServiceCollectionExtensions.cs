namespace Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.OpenApi.Models;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwagger();
        return builder;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
            });
        });
        return services;
    }

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString")
          ?? "Data Source=Tarefas.db";

        builder.Services.AddSqlite<TarefaDbContext>(connectionString);

        builder.Services.AddScoped(_ => new SqliteConnection(connectionString));

        return builder;
    }

    //public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    //{
    //    AddAuthenticationApp(builder.Services);
    //    return builder;
    //}

    //public static WebApplicationBuilder AddAuthorization(this WebApplicationBuilder builder)
    //{
    //    builder.Services.AddAuthorization(options =>
    //    {
    //        options.FallbackPolicy = new AuthorizationPolicyBuilder()
    //            .RequireAuthenticatedUser()
    //            .Build();
    //    });
    //    return builder;
    //}

    //private static IServiceCollection AddAuthenticationApp(this IServiceCollection services)
    //{
    //    services.AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    }).AddJwtBearer(options =>
    //    {
    //        options.Authority = "https://login.microsoftonline.com/xxxxxxxxxxxxxxxxxxxxxxxxxx";
    //        options.Audience = "xxxxxxxxxxxxxxxxxxxxxxxxx";
    //        options.TokenValidationParameters.ValidateLifetime = false;
    //        options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
    //    });

    //    return services;
    //}
}
