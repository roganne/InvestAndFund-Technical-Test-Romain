using InvestAndFund.Developer.Test.Api.Application.Output;
using InvestAndFund.Developer.Test.Api.Domain;
using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Payments;

public class BankTransferPaymentStrategy(IOrderProcessingOutput output) : IPaymentStrategy
{
    public bool CanHandle(string paymentType) =>
        paymentType.Equals(PaymentTypeEnum.BankTransfer, StringComparison.OrdinalIgnoreCase);

    public void Process(Order order)
    {
        output.Write("Processing bank transfer");
        output.Write($"Processing bank transfer {order.BankAccount} for {order.Amount}");
    }
}
