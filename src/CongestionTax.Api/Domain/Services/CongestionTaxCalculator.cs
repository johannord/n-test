using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Services;

public class CongestionTaxCalculator(IDateRules dateRules, IVehicleTypeRules vehicleTypeRules, ITaxRateRules taxRateRules) : ICongestionTaxCalculator
{
    public int GetTotalTax(Vehicle vehicle, IEnumerable<DateTime> passages)
    {
        if (vehicleTypeRules.IsTaxFreeVehicle(vehicle)) 
        {
            return 0;
        }

        var orderedPassages = passages.OrderBy(d => d);
        DateTime hourlyTaxationIntervalStart = orderedPassages.First();
        int totalTax = 0;
        int highestRateCurrentHourInterval = 0;

        foreach (DateTime passage in orderedPassages)
        {
            if (dateRules.IsTaxFreeDate(passage))
            {
                continue;
            }

            int taxRate = taxRateRules.GetTaxByPassage(passage);

            var minutesSinceIntervalStart = (passage - hourlyTaxationIntervalStart).TotalMinutes;

            if (minutesSinceIntervalStart <= 60)
            {
                if (taxRate >= highestRateCurrentHourInterval)
                {
                    if (totalTax > 0)
                    {
                        totalTax -= highestRateCurrentHourInterval;
                    }

                    highestRateCurrentHourInterval = taxRate;
                    totalTax += highestRateCurrentHourInterval;
                }
            }
            else
            {
                totalTax += taxRate;
                highestRateCurrentHourInterval = taxRate;
                hourlyTaxationIntervalStart = passage;
            }
        }

        if (totalTax > 60)
        {
            totalTax = 60;
        }

        return totalTax;
    }
}