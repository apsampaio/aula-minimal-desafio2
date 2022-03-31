using System.Security.Claims;
using API.Model;
using API.Errors;

namespace API.Service.Validations;

public static class ValidateUserClaims
{

    public static UserClaimProps GetClaimProps(ClaimsPrincipal user)
    {
        var id = user.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
        var name = user.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
        var role = user.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;

        if (id == null || name == null || role == null)
            throw new AppError("Unauthorized user", 401);

        var userClaimProps = new UserClaimProps
        {
            id = Guid.Parse(id),
            name = name,
            role = role
        };

        return userClaimProps;
    }

    public static void ValidateOwnerOrAdmin(UserClaimProps user, Guid userId)
    {
        if (user.id == userId || user.role == "Admin")
            return;

        throw new AppError("Permission denied", 403);

    }

}