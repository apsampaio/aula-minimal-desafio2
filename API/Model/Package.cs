using System.Text.Json.Serialization;


namespace API.Model;

public record Package
{
    public Guid id { get; init; }
    public string createdBy { get; init; } = default!;
    public Guid userId { get; init; }

    public DateTime updatedAt { get; set; }
    public Status status { get; set; }

    [JsonIgnore]
    public Details Details { get; init; } = default!;
}