using API.Service.Interfaces;
using API.Model;
using API.Infra.Repositories.Interfaces;
using API.Service.Validations;
using API.Errors;

namespace API.Service.Services;

class DetailsServiceCollection : IDetailsServiceCollection
{

    private IDetailsRepository _detailsRepository;

    public DetailsServiceCollection(IDetailsRepository detailsRepository)
    {
        _detailsRepository = detailsRepository;
    }

    public Details CreateDetails(Package package, ValidatePackageProps model)
    {

        var details = new Details
        {
            id = Guid.NewGuid(),
            houseNumber = model.houseNumber,
            recipient = model.recipient,
            zipcode = model.zipcode,
            postedAt = DateTime.UtcNow,
            deliveredAt = null,
            withdrawnAt = null,
            packageId = package.id,
        };

        _detailsRepository.Create(details);

        return details;
    }

    public Details FindDetailsByPackageId(Guid id)
    {
        var details = _detailsRepository.FindByPackageId(id);

        if (details == null)
            throw new AppError("Details not found", 404);

        return details;
    }

    public Details UpdateDetailsDateTime(Guid id)
    {
        var details = FindDetailsByPackageId(id);
        var status = details.Package.status;

        if ((int)status == (int)Status.transporting)
            details.withdrawnAt = DateTime.Now;
        if ((int)status == (int)Status.delivered)
            details.deliveredAt = DateTime.Now;

        _detailsRepository.Update(details);

        return details;
    }

    public void SaveChanges()
    {
        _detailsRepository.SaveChanges();
    }

}