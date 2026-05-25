namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

public interface ICustomerPricingStrategyFactory
{
    ICustomerPricingStrategy GetStrategy(string customerType);
}
