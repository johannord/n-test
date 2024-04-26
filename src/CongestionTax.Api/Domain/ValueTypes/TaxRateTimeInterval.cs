namespace CongestionTax.Api.Domain.ValueTypes;

public record TaxRateTimeInterval(TimeOnly From, TimeOnly To, int TaxAmount);