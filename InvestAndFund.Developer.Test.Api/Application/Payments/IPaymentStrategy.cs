using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Application.Payments;

public interface IPaymentStrategy
{
    bool CanHandle(string paymentType);
    void Process(Order order);
}
