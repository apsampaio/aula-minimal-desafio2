using API.Errors;
using API.Service.Validations;
using API.Service.Interfaces;
using System.Security.Claims;

namespace API.Routes;

public static class AppRoutes
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        app.MapGet("/package", List).RequireAuthorization("User");
        app.MapGet("/package/{id}", Find).RequireAuthorization("User");
        app.MapGet("/package/details/{id}", Details).RequireAuthorization("User");
        app.MapPost("/package", Create).RequireAuthorization("User");
        app.MapMethods("/package/status/{id}", new[] { "PATCH" }, Update).RequireAuthorization("User");
        app.MapMethods("/package/misplaced/{id}", new[] { "PATCH" }, Misplaced).RequireAuthorization("User");
        app.MapDelete("/package/{id}", Delete).RequireAuthorization("User");
        app.MapGet("/package/statistics", Base);
    }

    private static IResult List(IPackageServiceCollection packageCollection)
    {
        try
        {
            var packages = packageCollection.ListPackages();
            return Results.Ok(packages);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Find(IPackageServiceCollection packageCollection, Guid id)
    {
        try
        {
            var package = packageCollection.FindPackage(id);
            return Results.Ok(package);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Create(ValidatePackageProps model, IPackageServiceCollection packageCollection, IDetailsServiceCollection detailsCollection, ClaimsPrincipal claims)
    {
        try
        {
            model.Validate();

            var user = ValidateUserClaims.GetClaimProps(claims);

            var package = packageCollection.CreatePackage(user.id, user.name);
            detailsCollection.CreateDetails(package, model);

            packageCollection.SaveChanges();
            detailsCollection.SaveChanges();

            return Results.Ok(package);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Details(IDetailsServiceCollection detailsCollection, Guid id)
    {
        try
        {
            var details = detailsCollection.FindDetailsByPackageId(id);
            return Results.Ok(details);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Update(IPackageServiceCollection packageCollection, IDetailsServiceCollection detailsCollection, Guid id, ClaimsPrincipal claims)
    {
        try
        {
            var user = ValidateUserClaims.GetClaimProps(claims);
            var package = packageCollection.UpdatePackageStatus(id, user);
            detailsCollection.UpdateDetailsDateTime(id);
            return Results.Ok(package);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Misplaced(IPackageServiceCollection packageCollection, Guid id, ClaimsPrincipal claims)
    {
        try
        {
            var user = ValidateUserClaims.GetClaimProps(claims);
            var package = packageCollection.UpdatePackageMisplaced(id, user);
            return Results.Ok(package);
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Delete(IPackageServiceCollection packageCollection, Guid id, ClaimsPrincipal claims)
    {
        try
        {
            var user = ValidateUserClaims.GetClaimProps(claims);
            packageCollection.DeletePackage(id, user);
            return Results.NoContent();
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }

    private static IResult Base()
    {
        try
        {
            return Results.Ok();
        }
        catch (AppError ex)
        {
            return Results.Problem(statusCode: ex.statusCode, detail: ex.message);
        }
        catch
        {
            return Results.Problem(statusCode: 500, detail: "Internal Server Error");
        }
    }
}