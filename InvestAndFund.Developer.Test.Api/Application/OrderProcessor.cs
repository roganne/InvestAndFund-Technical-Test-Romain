using InvestAndFund.Developer.Test.Api.Application.Output;
using InvestAndFund.Developer.Test.Api.Application.Payments;
using InvestAndFund.Developer.Test.Api.Application.Pricing;
using InvestAndFund.Developer.Test.Api.Application.Validation;
using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Application;

public class OrderProcessor(
    IOrderValidator validator,
    ICustomerPricingStrategyFactory pricingFactory,
    IPaymentStrategyFactory paymentFactory,
    IOrderProcessingOutput output) : IOrderProcessor
{
    public void Process(Order order, string paymentType)
    {
        validator.ValidateOrThrow(order, paymentType);

        var pricingStrategy = pricingFactory.GetStrategy(order.CustomerType);
        order.Amount = pricingStrategy.ApplyDiscount(order.Amount);

        var paymentStrategy = paymentFactory.GetStrategy(paymentType);
        paymentStrategy.Process(order);

        output.Write("Saving order to database");
        output.Write($"Sending email to {order.CustomerEmail}");
    }
}
