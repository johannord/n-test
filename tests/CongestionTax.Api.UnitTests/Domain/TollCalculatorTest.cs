using CongestionTax.Api.Domain;
using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;

namespace CongestionTax.Api.UnitTests.Domain;

public class TollCalculatorTest
{
    private readonly TollCalculator _tollCalculator;

    public TollCalculatorTest()
    {
        VehicleRules vehicleRules = new();
        DateRules dateRules = new();

        _tollCalculator = new(vehicleRules, dateRules);
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
    [InlineData("2013-02-05 14:25:00", 8)]
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