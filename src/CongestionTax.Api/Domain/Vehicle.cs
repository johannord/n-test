namespace CongestionTax.Api.Domain;

public class Vehicle
{
    public VehicleType VehicleType { get; init; }

    public Vehicle(VehicleType vehicleType)
    {
        VehicleType = vehicleType;
    }
}