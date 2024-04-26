using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.ValueTypes;

namespace CongestionTax.Api.Domain.Rules;

public class TaxRateRules() : ITaxRateRules
{
    private readonly HashSet<TaxRateTimeInterval> _taxRateIntervals = [
        new TaxRateTimeInterval(new TimeOnly(0, 0), new TimeOnly(6, 0), 0),
        new TaxRateTimeInterval(new TimeOnly(6, 0), new TimeOnly(6, 30), 8),
        new TaxRateTimeInterval(new TimeOnly(6, 30), new TimeOnly(7, 0), 13),
        new TaxRateTimeInterval(new TimeOnly(7, 0), new TimeOnly(8, 0), 18),
        new TaxRateTimeInterval(new TimeOnly(8, 0), new TimeOnly(8, 30), 13),
        new TaxRateTimeInterval(new TimeOnly(8, 30), new TimeOnly(15, 0), 8),
        new TaxRateTimeInterval(new TimeOnly(15, 0), new TimeOnly(15, 30), 13),
        new TaxRateTimeInterval(new TimeOnly(15, 30), new TimeOnly(17, 0), 18),
        new TaxRateTimeInterval(new TimeOnly(17, 0), new TimeOnly(18, 0), 13),
        new TaxRateTimeInterval(new TimeOnly(18, 0), new TimeOnly(18, 30), 8),
        new TaxRateTimeInterval(new TimeOnly(18, 30), new TimeOnly(23, 59), 0)
    ];

    public int GetTaxByPassage(DateTime date)
    {
        var passageTime = TimeOnly.FromDateTime(date);

        var interval = _taxRateIntervals.FirstOrDefault(i => i.From <= passageTime && passageTime < i.To);

        return interval!.TaxAmount;
    }
}