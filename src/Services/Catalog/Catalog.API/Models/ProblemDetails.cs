namespace Catalog.API.Models
{
    public record ProblemDetails()
    {
        public string? Title { get; init; }
        public int? Status { get; init; }
        public string? Detail { get; init; }
    };

}
