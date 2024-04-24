using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Contracts;

public interface IVehicleTypeRules
{
    bool IsTaxFreeVehicle(Vehicle vehicle);
}