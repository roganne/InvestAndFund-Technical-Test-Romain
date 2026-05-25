namespace InvestAndFund.Developer.Test.Api.Application.Output;

public class OrderProcessingOutput(ILogger<OrderProcessingOutput> logger) : IOrderProcessingOutput
{
    public void Write(string message)
    {
        logger.LogInformation(message);
        Console.WriteLine(message);
    }
}
