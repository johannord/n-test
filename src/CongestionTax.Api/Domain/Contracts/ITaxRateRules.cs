using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Contracts;

public interface ITaxRateRules
{
    int GetTaxByPassage(DateTime date, Vehicle vehicle);
}