using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Services;

public class CongestionTaxCalculator(IDateRules dateRules, IVehicleTypeRules vehicleTypeRules, ITaxRateRules taxRateRules) : ICongestionTaxCalculator
{
    public int GetTotalTax(Vehicle vehicle, DateTime[] dates)
    {
        if (vehicleTypeRules.IsTaxFreeVehicle(vehicle)) return 0;

        var orderedPassages = dates.OrderBy(d => d);
        DateTime intervalStart = orderedPassages.First();
        int totalFee = 0;

        foreach (DateTime date in orderedPassages)
        {
            if (dateRules.IsTaxFreeDate(date))
                continue;

            int nextFee = taxRateRules.GetTaxByPassage(date);
            int tempFee = taxRateRules.GetTaxByPassage(intervalStart);

            var minutes = (date - intervalStart).TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
            }
        }

        if (totalFee > 60) totalFee = 60;

        return totalFee;
    }
}