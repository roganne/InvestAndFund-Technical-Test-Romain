using InvestAndFund.Developer.Test.Api.Domain;
using InvestAndFund.Developer.Test.Api.Domain.Enums;

namespace InvestAndFund.Developer.Test.Api.Application.Validation;

public class OrderValidator : IOrderValidator
{
    public bool TryValidate(Order? order, string? paymentType, out string errorMessage)
    {
        if (order is null)
        {
            errorMessage = "Order is null";
            return false;
        }

        if (order.Amount <= 0)
        {
            errorMessage = "Invalid amount";
            return false;
        }

        if (string.IsNullOrWhiteSpace(paymentType) || !PaymentTypeEnum.ValidPaymentTypes.Contains(paymentType))
        {
            errorMessage = "Unknown payment type";
            return false;
        }

        if (string.IsNullOrWhiteSpace(order.CustomerType))
        {
            errorMessage = "Customer type is required";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }

    public void ValidateOrThrow(Order order, string paymentType)
    {
        if (!TryValidate(order, paymentType, out var errorMessage))
        {
            throw new ArgumentException(errorMessage);
        }
    }
}
