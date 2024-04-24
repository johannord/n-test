namespace CongestionTax.Api.Domain.Contracts;

public interface IDateRules
{
    bool IsTaxFreeDate(DateTime date);
}