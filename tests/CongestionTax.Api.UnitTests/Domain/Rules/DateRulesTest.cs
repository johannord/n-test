using CongestionTax.Api.Domain.Rules;

namespace CongestionTax.Api.UnitTests.Domain.Rules;

public class DateRulesTest
{
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
    public void IsTaxFreeDate_PublicHoliday_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.True(isDateTaxFree);
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
    public void IsTaxFreeDate_DayBeforePublicHoliday_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString).AddDays(-1);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.True(isDateTaxFree);
    }

    [Theory]
    [InlineData("2013-07-01")]
    [InlineData("2013-07-12")]
    [InlineData("2013-07-31")]
    public void IsTaxFreeDate_MonthOfJuly_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.True(isDateTaxFree);
    }

    [Theory]
    [InlineData("2013-06-28")] // 30th is a Sunday
    [InlineData("2013-08-01")]
    public void IsTaxFreeDate_DaysAroundMonthOfJuly_ReturnsFalse(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.False(isDateTaxFree);
    }

    [Theory]
    [InlineData("2013-02-09")]
    [InlineData("2013-02-10")]
    public void IsTaxFreeDate_Weekend_ReturnsTrue(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.True(isDateTaxFree);
    }

    [Theory]
    [InlineData("2013-02-04")]
    [InlineData("2013-02-05")]
    [InlineData("2013-02-06")]
    [InlineData("2013-02-07")]
    [InlineData("2013-02-08")]
    public void IsTaxFreeDate_NotWeekend_ReturnsFalse(string dateString)
    {
        // arrange
        var date = DateTime.Parse(dateString);
        var dateRules = new DateRules();

        // act
        var isDateTaxFree = dateRules.IsTaxFreeDate(date);

        // assert
        Assert.False(isDateTaxFree);
    }
}