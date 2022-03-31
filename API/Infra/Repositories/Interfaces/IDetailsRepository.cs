using API.Model;

namespace API.Infra.Repositories.Interfaces;

public interface IDetailsRepository
{
    Details? FindById(Guid id);
    Details? FindByPackageId(Guid id);
    void Create(Details details);
    void Update(Details details);
    void SaveChanges();
}