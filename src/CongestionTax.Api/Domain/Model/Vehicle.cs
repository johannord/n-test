namespace CongestionTax.Api.Domain.Model;

public class Vehicle
{
    public VehicleType VehicleType { get; init; }

    public Vehicle(VehicleType vehicleType)
    {
        VehicleType = vehicleType;
    }
}