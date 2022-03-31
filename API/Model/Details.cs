namespace API.Model;

public record Details
{
    public Guid id { get; init; }
    public string recipient { get; init; } = default!;
    public string zipcode { get; init; } = default!;
    public Int32 houseNumber { get; init; }

    public Nullable<DateTime> postedAt { get; set; }
    public Nullable<DateTime> withdrawnAt { get; set; }
    public Nullable<DateTime> deliveredAt { get; set; }

    public Guid packageId { get; init; }

    public Package Package { get; init; } = default!;
}