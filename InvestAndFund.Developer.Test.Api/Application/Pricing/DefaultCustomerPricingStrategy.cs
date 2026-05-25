using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

/// <summary>
/// Full price for any customer that is not VIP or Loyal.
/// </summary>
public class DefaultCustomerPricingStrategy : ICustomerPricingStrategy
{
    public bool CanHandle(string customerType) =>
        !customerType.Equals(CustomerTypeEnum.Vip, StringComparison.OrdinalIgnoreCase) &&
        !customerType.Equals(CustomerTypeEnum.Loyal, StringComparison.OrdinalIgnoreCase);

    public decimal ApplyDiscount(decimal amount) => amount;
}
