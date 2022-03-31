using API.Model;
using Microsoft.EntityFrameworkCore;

using API.Infra.Context;
using API.Infra.Repositories.Interfaces;

namespace API.Infra.Repositories;

public class PackageRepository : IPackageRepository
{

    private readonly DatabaseContext _databaseContext;
    private DbSet<Package> entity;

    public PackageRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        entity = _databaseContext.Set<Package>();
    }

    public Package? FindById(Guid id)
    {
        var package = entity.SingleOrDefault(p => p.id == id);
        return package;
    }

    public void Create(Package package)
    {
        entity.Add(package);
    }

    public IEnumerable<Package> List()
    {
        var packages = entity.ToList();
        return packages;
    }

    public void Update(Package package)
    {
        entity.Update(package);
        SaveChanges();
    }

    public void Delete(Package package)
    {
        entity.Remove(package);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _databaseContext.SaveChanges();
    }

}