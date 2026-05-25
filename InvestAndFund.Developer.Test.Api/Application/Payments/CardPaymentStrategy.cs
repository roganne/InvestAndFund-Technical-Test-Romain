using InvestAndFund.Developer.Test.Api.Application.Output;
using InvestAndFund.Developer.Test.Api.Domain;
using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Payments;

public class CardPaymentStrategy(IOrderProcessingOutput output) : IPaymentStrategy
{
    public bool CanHandle(string paymentType) =>
        paymentType.Equals(PaymentTypeEnum.Card, StringComparison.OrdinalIgnoreCase);

    public void Process(Order order)
    {
        output.Write("Processing card payment");
        output.Write($"Charging card {order.CardNumber} for {order.Amount}");
    }
}
