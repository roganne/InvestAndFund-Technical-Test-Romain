using InvestAndFund.Developer.Test.Api.Application;
using InvestAndFund.Developer.Test.Api.Application.Output;
using InvestAndFund.Developer.Test.Api.Application.Payments;
using InvestAndFund.Developer.Test.Api.Application.Pricing;
using InvestAndFund.Developer.Test.Api.Application.Validation;
using InvestAndFund.Developer.Test.Api.Domain;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace InvestAndFund.Developer.Test.Tests;

[CollectionDefinition("OrderProcessorTests", DisableParallelization = true)]
public class OrderProcessorTestsCollection;

[Collection("OrderProcessorTests")]
public class OrderProcessorTests
{
    private const decimal Amount = 100m;
    private const decimal VipPayable = 80m;

    private readonly OrderProcessor _op = CreateProcessor();

    [Fact]
    public void Process_NullOrder_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => _op.Process(null!, "Card"));
        Assert.Equal("Order is null", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Process_InvalidAmount_Throws(decimal amount)
    {
        var ex = Assert.Throws<ArgumentException>(() => _op.Process(CreateOrder(amount: amount), "Card"));
        Assert.Equal("Invalid amount", ex.Message);
    }

    [Fact]
    public void Process_UnknownPaymentType_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => _op.Process(CreateOrder(), "PayPal"));
        Assert.Equal("Unknown payment type", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Process_MissingCustomerType_Throws(string? customerType)
    {
        var order = CreateOrder();
        order.CustomerType = customerType!;
        var ex = Assert.Throws<ArgumentException>(() => _op.Process(order, "Card"));
        Assert.Equal("Customer type is required", ex.Message);
    }

    [Fact]
    public void Process_Card_ChargesCard()
    {
        var order = CreateOrder();
        var output = CaptureOutput(() => _op.Process(order, "Card"));

        Assert.Contains("Processing card payment", output);
        Assert.Contains($"Charging card {order.CardNumber} for {Amount}", output);
    }

    [Fact]
    public void Process_BankTransfer_ChargesAccount()
    {
        var order = CreateOrder();
        var output = CaptureOutput(() => _op.Process(order, "BankTransfer"));

        Assert.Contains("Processing bank transfer", output);
        Assert.Contains($"Processing bank transfer {order.BankAccount} for {Amount}", output);
    }

    [Theory]
    [InlineData("Retail")]
    [InlineData("Corporate")]
    public void Process_NonDiscountedCustomer_PaysFullAmount(string customerType)
    {
        var order = CreateOrder(customerType);
        var output = CaptureOutput(() => _op.Process(order, "Card"));

        Assert.Contains($"for {Amount}", output);
        Assert.Equal(Amount, order.Amount);
    }

    [Theory]
    [InlineData("VIP", 80)]
    [InlineData("Loyal", 50)]
    public void Process_DiscountedCustomer_ChargesAndPersistsDiscount(string customerType, decimal expected)
    {
        var order = CreateOrder(customerType);
        var output = CaptureOutput(() => _op.Process(order, "Card"));

        Assert.Contains($"for {expected}", output);
        Assert.Equal(expected, order.Amount);
    }

    [Fact]
    public void Process_Vip_BankTransfer_UsesDiscountedAmount()
    {
        var order = CreateOrder("VIP");
        var output = CaptureOutput(() => _op.Process(order, "BankTransfer"));
        Assert.Contains($"for {VipPayable}", output);
    }

    [Fact]
    public void Process_Vip_DoesNotChargeFullAmount()
    {
        var output = CaptureOutput(() => _op.Process(CreateOrder("VIP"), "Card"));
        Assert.Contains($"for {VipPayable}", output);
        Assert.DoesNotContain($"for {Amount}", output);
    }

    [Fact]
    public void Process_SavesAndEmailsOnce()
    {
        var order = CreateOrder();
        var output = CaptureOutput(() => _op.Process(order, "Card"));

        Assert.Equal(1, output.Split("Saving order to database").Length - 1);
        Assert.Equal(1, output.Split($"Sending email to {order.CustomerEmail}").Length - 1);
    }

    private static OrderProcessor CreateProcessor()
    {
        var output = new OrderProcessingOutput(NullLogger<OrderProcessingOutput>.Instance);
        return new OrderProcessor(
            new OrderValidator(),
            new CustomerPricingStrategyFactory(
            [
                new VipCustomerPricingStrategy(),
                new LoyalCustomerPricingStrategy(),
                new DefaultCustomerPricingStrategy()
            ]),
            new PaymentStrategyFactory(
            [
                new CardPaymentStrategy(output),
                new BankTransferPaymentStrategy(output)
            ]),
            output);
    }

    private static Order CreateOrder(string customerType = "Retail", decimal amount = Amount) =>
        new()
        {
            Amount = amount,
            CustomerType = customerType,
            CustomerEmail = "customer@example.com",
            CardNumber = "4111111111111111",
            BankAccount = "GB00TEST123456789"
        };

    private static string CaptureOutput(Action action)
    {
        var writer = new StringWriter();
        var previous = Console.Out;
        try
        {
            Console.SetOut(writer);
            action();
            return writer.ToString();
        }
        finally
        {
            Console.SetOut(previous);
            writer.Dispose();
        }
    }
}
