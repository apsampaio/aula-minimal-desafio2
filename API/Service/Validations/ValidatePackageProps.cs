using Flunt.Notifications;
using Flunt.Validations;
using API.Errors;

namespace API.Service.Validations;

public class ValidatePackageProps : Notifiable<Notification>
{
    public string recipient { get; init; } = default!;
    public string zipcode { get; init; } = default!;
    public Int32 houseNumber { get; init; }

    private void MapTo()
    {
        var contract = new Contract<Notification>()
        .Requires()
        .IsNotNullOrEmpty(recipient, "Informe o remetente.")
        .IsGreaterThan(recipient, 2, "Remetente deve ter no mínimo 3 caracteres.")
        .IsNotNull(houseNumber, "Informe o número da residência.")
        .IsNotNullOrEmpty(zipcode, "Informe o CEP.")
        .Matches(zipcode, @"^[0-9]{5}[0-9]{3}$", "CEP inválido.");

        AddNotifications(contract);

        return;
    }

    public void Validate()
    {
        MapTo();

        if (!IsValid)
        {
            var message = Notifications.First().Key;
            throw new AppError(message, 400);
        }
    }

}