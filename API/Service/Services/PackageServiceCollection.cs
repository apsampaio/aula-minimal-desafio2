using API.Model;
using API.Infra.Repositories.Interfaces;
using API.Service.Interfaces;
using API.Errors;
using API.Service.Validations;

namespace API.Service.Services;

class PackageServiceCollection : IPackageServiceCollection
{
    private readonly IPackageRepository _packageRepository;

    public PackageServiceCollection(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public IEnumerable<Package> ListPackages()
    {
        return _packageRepository.List();
    }

    public Package FindPackage(Guid id)
    {
        var package = _packageRepository.FindById(id);

        if (package == null) throw new AppError("Package not found", 404);

        return package;
    }

    public Package CreatePackage(Guid userId, string userName)
    {
        var package = new Package
        {
            id = Guid.NewGuid(),
            createdBy = userName,
            userId = userId,
            status = Status.waiting,
            updatedAt = DateTime.UtcNow,
        };

        _packageRepository.Create(package);

        return package;
    }

    public Package UpdatePackageStatus(Guid id, UserClaimProps user)
    {
        var package = FindPackage(id);

        ValidateUserClaims.ValidateOwnerOrAdmin(user, package.userId);

        var status = (int)package.status;

        if (status > (int)Status.transporting)
            throw new AppError("Package status cannot be updated", 400);


        package.status = (Status)(status + 1);
        package.updatedAt = DateTime.Now;

        _packageRepository.Update(package);

        return package;
    }

    public Package UpdatePackageMisplaced(Guid id, UserClaimProps user)
    {
        var package = FindPackage(id);
        ValidateUserClaims.ValidateOwnerOrAdmin(user, package.userId);

        package.status = Status.misplaced;

        _packageRepository.Update(package);

        return package;
    }

    public void DeletePackage(Guid id, UserClaimProps user)
    {
        var package = FindPackage(id);
        ValidateUserClaims.ValidateOwnerOrAdmin(user, package.userId);

        _packageRepository.Delete(package);

        return;
    }

    public void SaveChanges()
    {
        _packageRepository.SaveChanges();
    }

}