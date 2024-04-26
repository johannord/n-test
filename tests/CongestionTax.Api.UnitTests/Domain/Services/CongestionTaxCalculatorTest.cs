using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;
using CongestionTax.Api.Domain.Services;

namespace CongestionTax.Api.UnitTests.Domain.Services;

public class CongestionTaxCalculatorTest
{
    [Fact]
    public void GetTotalTax_VehicleIsCarTaxableDate_ReturnsMaxTotalTax()
    {
        // arrange
        DateRules dateRules = new();
        VehicleTypeRules vehicleTypeRules = new();
        TaxRateRules taxRateRules = new();
        CongestionTaxCalculator taxCalculator = new(dateRules, vehicleTypeRules, taxRateRules);
        IEnumerable<DateTime> passageTimestamps = [
            DateTime.Parse("2013-06-11 06:29:00"), // 0
            DateTime.Parse("2013-06-11 06:22:00"), // + 8
            DateTime.Parse("2013-06-11 14:35:00"), // 0
            DateTime.Parse("2013-06-11 15:29:00"), // + 13
            DateTime.Parse("2013-06-11 15:42:00"), // + 18
            DateTime.Parse("2013-06-11 16:01:00"), // 0
            DateTime.Parse("2013-06-11 16:43:00"), // + 18
            DateTime.Parse("2013-06-11 17:44:00"), // + 13
            DateTime.Parse("2013-06-11 18:29:00"), // 0
            DateTime.Parse("2013-06-11 18:55:00")  // 0
        ];
        Vehicle vehicle = new(VehicleType.Car);

        // act
        var totalTax = taxCalculator.GetTotalTax(vehicle, passageTimestamps);

        // assert
        Assert.Equal(60, totalTax);
    }

    [Fact]
    public void GetTotalTax_PassagesOccuringWithin60minutes_ReturnsHighestTaxAmount()
    {
        // arrange
        DateRules dateRules = new();
        VehicleTypeRules vehicleTypeRules = new();
        TaxRateRules taxRateRules = new();
        CongestionTaxCalculator taxCalculator = new(dateRules, vehicleTypeRules, taxRateRules);
        IEnumerable<DateTime> passageTimestamps = [
            DateTime.Parse("2013-06-11 14:58:00"),
            DateTime.Parse("2013-06-11 14:59:00"),
            DateTime.Parse("2013-06-11 15:01:00"),
            DateTime.Parse("2013-06-11 15:15:00"),
            DateTime.Parse("2013-06-11 15:29:00"),
            DateTime.Parse("2013-06-11 15:31:00"),
            DateTime.Parse("2013-06-11 15:57:00"),
        ];
        Vehicle vehicle = new(VehicleType.Car);

        // act
        var totalTax = taxCalculator.GetTotalTax(vehicle, passageTimestamps);

        // assert
        Assert.Equal(18, totalTax);
    }

    [Fact]
    public void GetTotalTax_PassagesOccuringWithin120minutes_ReturnsCorrectTotalTax()
    {
        // arrange
        DateRules dateRules = new();
        VehicleTypeRules vehicleTypeRules = new();
        TaxRateRules taxRateRules = new();
        CongestionTaxCalculator taxCalculator = new(dateRules, vehicleTypeRules, taxRateRules);
        IEnumerable<DateTime> passageTimestamps = [
            DateTime.Parse("2013-06-11 14:21:00"),
            DateTime.Parse("2013-06-11 14:59:00"),
            DateTime.Parse("2013-06-11 15:01:00"),
            DateTime.Parse("2013-06-11 15:15:00"),
            DateTime.Parse("2013-06-11 15:29:00"),
            DateTime.Parse("2013-06-11 15:31:00"),
            DateTime.Parse("2013-06-11 15:57:00"),
            DateTime.Parse("2013-06-11 16:01:00"),
            DateTime.Parse("2013-06-11 16:21:00"),
        ];
        Vehicle vehicle = new(VehicleType.Car);

        // act
        var totalTax = taxCalculator.GetTotalTax(vehicle, passageTimestamps);

        // assert
        Assert.Equal(31, totalTax);
    }
}