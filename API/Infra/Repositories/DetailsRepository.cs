using API.Model;
using Microsoft.EntityFrameworkCore;

using API.Infra.Context;
using API.Infra.Repositories.Interfaces;

namespace API.Infra.Repositories;

public class DetailsRepository : IDetailsRepository
{

    private readonly DatabaseContext _databaseContext;
    private DbSet<Details> entity;
    private DbSet<Package> packageEntity;

    public DetailsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        entity = _databaseContext.Set<Details>();
        packageEntity = _databaseContext.Set<Package>();
    }

    public Details? FindById(Guid id)
    {
        var details = entity.SingleOrDefault(d => d.id == id);
        return details;
    }

    public Details? FindByPackageId(Guid id)
    {
        var details = entity.Include(d => d.Package).SingleOrDefault(d => d.packageId == id);

        return details;
    }

    public void Create(Details details)
    {
        entity.Add(details);
    }

    public void Update(Details details)
    {
        entity.Update(details);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _databaseContext.SaveChanges();
    }

}