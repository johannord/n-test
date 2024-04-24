using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Rules;

public class VehicleRules : IVehicleRules
{
    private readonly HashSet<VehicleType> _tollFreeVehicles = [
        VehicleType.Motorbike,
        VehicleType.Tractor,
        VehicleType.Emergency,
        VehicleType.Diplomat,
        VehicleType.Foreign,
        VehicleType.Military
    ];

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (_tollFreeVehicles.Contains(vehicle.VehicleType))
        {
            return true;
        }

        return false;
    }
}