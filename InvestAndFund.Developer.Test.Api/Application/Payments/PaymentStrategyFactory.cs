namespace InvestAndFund.Developer.Test.Api.Application.Payments;

public class PaymentStrategyFactory(IEnumerable<IPaymentStrategy> strategies) : IPaymentStrategyFactory
{
    public IPaymentStrategy GetStrategy(string paymentType)
    {
        var strategy = strategies.FirstOrDefault(s => s.CanHandle(paymentType));
        if (strategy is null)
        {
            throw new ArgumentException("Unknown payment type", nameof(paymentType));
        }

        return strategy;
    }
}
