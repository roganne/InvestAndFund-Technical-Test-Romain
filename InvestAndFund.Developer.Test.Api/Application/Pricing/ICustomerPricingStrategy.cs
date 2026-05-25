namespace InvestAndFund.Developer.Test.Api.Application.Pricing;

public interface ICustomerPricingStrategy
{
    bool CanHandle(string customerType);
    decimal ApplyDiscount(decimal amount);
}
