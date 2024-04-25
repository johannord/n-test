using System.Text.Json.Serialization;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Application.Contracts;

public record CongestionTaxCalculationRequest
{
    public required VehicleRequest Vehicle { get; init; } 
    public required IEnumerable<DateTime> PassageTimestamps { get; init; } 
}

public record VehicleRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VehicleType Type { get; init; }
}