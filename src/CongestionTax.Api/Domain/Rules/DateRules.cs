using CongestionTax.Api.Domain.Contracts;

namespace CongestionTax.Api.Domain.Rules;

public class DateRules : IDateRules
{
    private readonly HashSet<DateTime> _publicHolidays =
    [
        new DateTime(2013, 1, 1),
        new DateTime(2013, 1, 6),
        new DateTime(2013, 3, 29),
        new DateTime(2013, 3, 30),
        new DateTime(2013, 4, 1),
        new DateTime(2013, 5, 1),
        new DateTime(2013, 5, 9),
        new DateTime(2013, 5, 19),
        new DateTime(2013, 6, 6),
        new DateTime(2013, 6, 22),
        new DateTime(2013, 11, 2),
        new DateTime(2013, 12, 25),
        new DateTime(2013, 12, 26),
        new DateTime(2013, 12, 31)
    ];

    private readonly int _publicHolidayOffset = -1;

    private readonly HashSet<int> _exemptMonths = [ 7 ];

    private readonly HashSet<DayOfWeek> _exemptWeekdays = [ DayOfWeek.Saturday, DayOfWeek.Sunday ];

    public bool IsTaxFreeDate(DateTime date)
    {
        if (_exemptWeekdays.Contains(date.DayOfWeek))
        {
            return true;
        }
        else if (_exemptMonths.Contains(date.Month))
        {
            return true;
        }
        else if (_publicHolidays.Select(o => o.Date).Contains(date.Date) == true)
        {
            return true;
        }
        else if (_publicHolidays.Select(o => o.Date.AddDays(_publicHolidayOffset)).Contains(date.Date) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}