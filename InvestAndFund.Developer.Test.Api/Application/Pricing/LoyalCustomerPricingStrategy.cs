using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

/// <summary>
/// Loyal customers receive a 50% discount (pay 50% of the original amount).
/// </summary>
public class LoyalCustomerPricingStrategy : ICustomerPricingStrategy
{
    /// <summary>Payable fraction after a 50% discount.</summary>
    public const decimal PayableFraction = 0.5m;

    public bool CanHandle(string customerType) =>
        customerType.Equals(CustomerTypeEnum.Loyal, StringComparison.OrdinalIgnoreCase);

    public decimal ApplyDiscount(decimal amount) => amount * PayableFraction;
}
