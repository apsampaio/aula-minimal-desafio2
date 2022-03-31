using API.Infra.Context;

using API.Infra.Repositories;
using API.Infra.Repositories.Interfaces;

using API.Service.Services;
using API.Service.Interfaces;

using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API;

public static class ServiceRegister
{

    public static void RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();
    }

    public static void DependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IPackageRepository), typeof(PackageRepository));
        services.AddTransient<IPackageServiceCollection, PackageServiceCollection>();

        services.AddSingleton(typeof(IDetailsRepository), typeof(DetailsRepository));
        services.AddTransient<IDetailsServiceCollection, DetailsServiceCollection>();
    }

    public static void ConfigureCORS(this IServiceCollection services)
    {
        services.AddCors(opt =>
            opt.AddPolicy("CORS", builder =>
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .AllowCredentials()
            )
        );
    }

    public static void RegisterAuthentication(this IServiceCollection services)
    {
        var secret = "MY_BIGGEST_SECRET";
        var key = Encoding.ASCII.GetBytes(secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }
    public static void RegisterAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireRole("Admin"));
            options.AddPolicy("User", policy =>
                policy.RequireRole("User"));
        });
    }

}