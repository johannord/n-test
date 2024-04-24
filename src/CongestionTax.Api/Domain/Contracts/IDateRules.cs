namespace CongestionTax.Api.Domain.Contracts;

public interface IDateRules
{
    bool IsTollFreeDate(DateTime date);
}