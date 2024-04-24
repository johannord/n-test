using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;

namespace CongestionTax.Api.UnitTests.Domain.Rules;

public class VehicleRulesTest
{
    [Theory]
    [InlineData(VehicleType.Emergency)]
    [InlineData(VehicleType.Diplomat)]
    [InlineData(VehicleType.Motorbike)]
    [InlineData(VehicleType.Military)]
    [InlineData(VehicleType.Foreign)]
    [InlineData(VehicleType.Tractor)]
    public void IsTollFreeVehicle_ExemptVehicles_ReturnsTrue(VehicleType vehicleType)
    {
        // arrange
        var vehicle = new Vehicle(vehicleType);
        var tollCalculator = new VehicleRules();

        // act
        var isTollFreeVehicle = tollCalculator.IsTollFreeVehicle(vehicle);

        // assert
        Assert.True(isTollFreeVehicle);
    }

    [Theory]
    [InlineData(VehicleType.Car)]
    public void IsTollFreeVehicle_NonExemptVehicles_ReturnsFalse(VehicleType vehicleType)
    {
        // arrange
        var vehicle = new Vehicle(vehicleType);
        var tollCalculator = new VehicleRules();

        // act
        var isTollFreeVehicle = tollCalculator.IsTollFreeVehicle(vehicle);

        // assert
        Assert.False(isTollFreeVehicle);
    }
}