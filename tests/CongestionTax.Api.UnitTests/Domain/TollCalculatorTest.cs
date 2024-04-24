using CongestionTax.Api.Domain;
using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;

namespace CongestionTax.Api.UnitTests.Domain;

public class TollCalculatorTest
{
    private readonly TollCalculator _tollCalculator;

    public TollCalculatorTest()
    {
        var vehicleRules = new VehicleRules();
        _tollCalculator = new(vehicleRules);
    }

    [Theory]
    [InlineData("2013-01-01")]
    [InlineData("2013-01-06")]
    [InlineData("2013-03-29")]
    [InlineData("2013-04-01")]
    [InlineData("2013-05-01")]
    [InlineData("2013-05-09")]
    [InlineData("2013-05-19")]
    [InlineData("2013-06-06")]
    [InlineData("2013-06-22")]
    [InlineData("2013-11-02")]
    [InlineData("2013-12-25")]
    [InlineData("2013-12-26")]
    public void IsTollFreeDate_PublicHoliday_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.True(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-01-01")] // fails, existing implementation only regards 2013
    [InlineData("2013-01-06")]
    [InlineData("2013-03-29")]
    [InlineData("2013-04-01")]
    [InlineData("2013-05-01")]
    [InlineData("2013-05-09")]
    [InlineData("2013-05-19")]
    [InlineData("2013-06-06")]
    [InlineData("2013-06-22")]
    [InlineData("2013-11-02")]
    [InlineData("2013-12-25")]
    [InlineData("2013-12-26")]
    public void IsTollFreeDate_DayBeforePublicHoliday_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString).AddDays(-1);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.True(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-07-01")]
    [InlineData("2013-07-12")]
    [InlineData("2013-07-31")]
    public void IsTollFreeDate_MonthOfJuly_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.True(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-06-28")] // 30th is a Sunday
    [InlineData("2013-08-01")]
    public void IsTollFreeDate_DaysAroundMonthOfJuly_ReturnsFalse(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.False(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-02-09")]
    [InlineData("2013-02-10")]
    public void IsTollFreeDate_Weekend_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.True(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-02-04")]
    [InlineData("2013-02-05")]
    [InlineData("2013-02-06")]
    [InlineData("2013-02-07")]
    [InlineData("2013-02-08")]
    public void IsTollFreeDate_NotWeekend_ReturnsFalse(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);

        // act
        var isDateToolFree = _tollCalculator.IsTollFreeDate(date);

        // assert
        Assert.False(isDateToolFree);
    }

    [Theory]
    [InlineData("2013-02-05 05:59:59", 0)]
    [InlineData("2013-02-05 06:00:00", 8)]
    [InlineData("2013-02-05 06:29:59", 8)]
    [InlineData("2013-02-05 06:30:00", 13)]
    [InlineData("2013-02-05 06:59:59", 13)]
    [InlineData("2013-02-05 07:00:00", 18)]
    [InlineData("2013-02-05 07:59:59", 18)]
    [InlineData("2013-02-05 08:00:00", 13)]
    [InlineData("2013-02-05 08:29:59", 13)]
    [InlineData("2013-02-05 08:30:00", 8)]
    [InlineData("2013-02-05 14:25:00", 8)] // fails, bug?
    [InlineData("2013-02-05 14:59:59", 8)]
    [InlineData("2013-02-05 15:00:00", 13)]
    [InlineData("2013-02-05 15:29:59", 13)]
    [InlineData("2013-02-05 15:30:00", 18)]
    [InlineData("2013-02-05 16:59:59", 18)]
    [InlineData("2013-02-05 17:00:00", 13)]
    [InlineData("2013-02-05 17:59:59", 13)]
    [InlineData("2013-02-05 18:00:00", 8)]
    [InlineData("2013-02-05 18:29:59", 8)]
    [InlineData("2013-02-05 18:30:00", 0)]
    public void GetTollFee_Timestamp_ReturnsCorrectTax(string dateString, int expectedTax)
    {
        // arrange
        var vehicle = new Vehicle(VehicleType.Car);
        var timestamp = DateTime.Parse(dateString);

        // act
        var tax = _tollCalculator.GetTollFee(timestamp, vehicle);

        // assert
        Assert.Equal(expectedTax, tax);
    }
}