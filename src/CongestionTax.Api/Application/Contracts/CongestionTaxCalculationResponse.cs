namespace CongestionTax.Api.Application.Contracts;

public record CongestionTaxCalculationResponse
{
    public required int TotalTax { get; init; } 
}
