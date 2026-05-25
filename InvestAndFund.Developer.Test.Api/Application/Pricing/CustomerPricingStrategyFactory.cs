namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

public class CustomerPricingStrategyFactory(IEnumerable<ICustomerPricingStrategy> strategies) : ICustomerPricingStrategyFactory
{
    public ICustomerPricingStrategy GetStrategy(string customerType)
    {
        var strategy = strategies.FirstOrDefault(s => s.CanHandle(customerType));
        if (strategy is null)
        {
            throw new ArgumentException("Unknown customer type", nameof(customerType));
        }

        return strategy;
    }
}
