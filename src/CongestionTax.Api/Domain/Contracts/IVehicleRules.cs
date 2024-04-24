using CongestionTax.Api.Domain.Model;

namespace CongestionTax.Api.Domain.Contracts;

public interface IVehicleRules
{
    bool IsTaxFreeVehicle(Vehicle vehicle);
}