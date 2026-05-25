using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Application;

public interface IOrderProcessor
{
    void Process(Order order, string paymentType);
}
