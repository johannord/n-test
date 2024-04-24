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
    public void IsTaxFreeVehicle_ExemptVehicles_ReturnsTrue(VehicleType vehicleType)
    {
        // arrange
        var vehicle = new Vehicle(vehicleType);
        var vehicleRules = new VehicleRules();

        // act
        var isTaxFreeVehicle = vehicleRules.IsTaxFreeVehicle(vehicle);

        // assert
        Assert.True(isTaxFreeVehicle);
    }

    [Theory]
    [InlineData(VehicleType.Car)]
    public void IsTaxFreeVehicle_NonExemptVehicles_ReturnsFalse(VehicleType vehicleType)
    {
        // arrange
        var vehicle = new Vehicle(vehicleType);
        var vehicleRules = new VehicleRules();

        // act
        var isTaxFreeVehicle = vehicleRules.IsTaxFreeVehicle(vehicle);

        // assert
        Assert.False(isTaxFreeVehicle);
    }
}