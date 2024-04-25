using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Contracts;

public interface ICongestionTaxCalculator
{
    int GetTotalTax(Vehicle vehicle, DateTime[] dates);
}