namespace API.Model;

public record UserClaimProps
{
    public Guid id { get; init; }
    public string name { get; init; } = default!;
    public string role { get; init; } = default!;
}