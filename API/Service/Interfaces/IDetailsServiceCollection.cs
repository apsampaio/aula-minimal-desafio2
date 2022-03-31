using API.Model;
using API.Service.Validations;

namespace API.Service.Interfaces;


public interface IDetailsServiceCollection
{
    Details CreateDetails(Package package, ValidatePackageProps model);
    Details FindDetailsByPackageId(Guid id);
    Details UpdateDetailsDateTime(Guid id);
    void SaveChanges();
}