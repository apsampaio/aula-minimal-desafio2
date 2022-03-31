using API.Model;
namespace API.Service.Interfaces;

public interface IPackageServiceCollection
{
    IEnumerable<Package> ListPackages();
    Package FindPackage(Guid id);
    Package CreatePackage(Guid userId, string userName);
    Package UpdatePackageStatus(Guid id, UserClaimProps user);
    Package UpdatePackageMisplaced(Guid id, UserClaimProps user);
    void DeletePackage(Guid id, UserClaimProps user);
    void SaveChanges();
}