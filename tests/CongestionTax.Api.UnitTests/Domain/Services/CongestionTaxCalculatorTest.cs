using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;
using CongestionTax.Api.Domain.Services;

namespace CongestionTax.Api.UnitTests.Domain.Services;

public class CongestionTaxCalculatorTest
{
    [Fact]
    public void GetTotalTax_VehicleIsCarTaxableDate_ReturnsTotalTax()
    {
        // arrange
        DateRules dateRules = new();
        VehicleTypeRules vehicleTypeRules = new();
        TaxRateRules taxRateRules = new();
        CongestionTaxCalculator taxCalculator = new(dateRules, vehicleTypeRules, taxRateRules);
        DateTime[] passageTimestamps = [
            DateTime.Parse("2013-06-11 06:29:00"), // 0
            DateTime.Parse("2013-06-11 06:22:00"), // + 8
            DateTime.Parse("2013-06-11 14:35:00"), // 0
            DateTime.Parse("2013-06-11 15:29:00"), // + 13
            DateTime.Parse("2013-06-11 15:42:00"), // + 18
            DateTime.Parse("2013-06-11 16:01:00"), // 0
            DateTime.Parse("2013-06-11 16:43:00"), // + 18
            DateTime.Parse("2013-06-11 17:44:00"), // + 13
            DateTime.Parse("2013-06-11 18:29:00"), // 0
            DateTime.Parse("2013-06-11 18:35:00")  // 0
        ];
        Vehicle vehicle = new(VehicleType.Car);

        // act
        var totalTax = taxCalculator.GetTotalTax(vehicle, passageTimestamps);

        // assert
        Assert.Equal(60, totalTax);
    }
}