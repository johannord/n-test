using CongestionTax.Api.Application.Contracts;
using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Application;

public class CalculateCongestionTaxHandler(ICongestionTaxCalculator taxCalculator)
{
    public CongestionTaxCalculationResponse Handle(CongestionTaxCalculationRequest request)
    {
        Vehicle vehicle = new(request.Vehicle.Type);

        var totalTax = taxCalculator.GetTotalTax(vehicle, request.PassageTimestamps);

        return new CongestionTaxCalculationResponse() { TotalTax = totalTax };
    }
}