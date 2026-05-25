namespace InvestAndFund.Developer.Test.Api.Application.Payments;

public interface IPaymentStrategyFactory
{
    IPaymentStrategy GetStrategy(string paymentType);
}
