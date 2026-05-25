using InvestAndFund.Developer.Test.Api.Application.Output;
using InvestAndFund.Developer.Test.Api.Application.Payments;
using InvestAndFund.Developer.Test.Api.Application.Pricing;
using InvestAndFund.Developer.Test.Api.Application.Validation;
using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderProcessing(this IServiceCollection services)
    {
        services.AddSingleton<IOrderValidator, OrderValidator>();
        services.AddSingleton<IOrderProcessingOutput, OrderProcessingOutput>();

        services.AddSingleton<ICustomerPricingStrategy, VipCustomerPricingStrategy>();
        services.AddSingleton<ICustomerPricingStrategy, LoyalCustomerPricingStrategy>();
        services.AddSingleton<ICustomerPricingStrategy, DefaultCustomerPricingStrategy>();
        services.AddSingleton<ICustomerPricingStrategyFactory, CustomerPricingStrategyFactory>();

        services.AddSingleton<IPaymentStrategy, CardPaymentStrategy>();
        services.AddSingleton<IPaymentStrategy, BankTransferPaymentStrategy>();
        services.AddSingleton<IPaymentStrategyFactory, PaymentStrategyFactory>();

        services.AddScoped<IOrderProcessor, OrderProcessor>();

        return services;
    }
}
