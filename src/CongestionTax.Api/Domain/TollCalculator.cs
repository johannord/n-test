namespace CongestionTax.Api.Domain;

using System;
using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

public class TollCalculator(IVehicleRules vehicleRules)
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

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies/1000/60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || vehicleRules.IsTollFreeVehicle(vehicle)) return 0;

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

    public bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }
        else if (date.Month == 7)
        {
            return true;
        }
        else if (_publicHolidays.Select(o => o.Date).Contains(date.Date) == true)
        {
            return true;
        }
        else if (_publicHolidays.Select(o => o.Date.AddDays(-1)).Contains(date.Date) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}