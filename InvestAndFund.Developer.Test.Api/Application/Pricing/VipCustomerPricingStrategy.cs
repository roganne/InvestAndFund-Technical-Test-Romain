using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

public class VipCustomerPricingStrategy : ICustomerPricingStrategy
{
    public bool CanHandle(string customerType) =>
        customerType.Equals(CustomerTypeEnum.Vip, StringComparison.OrdinalIgnoreCase);

    public decimal ApplyDiscount(decimal amount) => amount * 0.8m;
}
