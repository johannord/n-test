using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Rules;

public class TaxRateRules(IDateRules dateRules, IVehicleTypeRules vehicleTypeRules) : ITaxRateRules
{
    public int GetTaxByPassage(DateTime date, Vehicle vehicle)
    {
        if (dateRules.IsTaxFreeDate(date) || vehicleTypeRules.IsTaxFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 30) return 18;
        else if (hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }
}