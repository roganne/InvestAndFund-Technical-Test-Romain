using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Application.Validation;

public interface IOrderValidator
{
    /// <summary>
    /// Returns false and an error message when the request cannot be processed.
    /// </summary>
    bool TryValidate(Order? order, string? paymentType, out string errorMessage);

    /// <summary>
    /// Throws when the order or payment type is invalid. Used by the processing pipeline.
    /// </summary>
    void ValidateOrThrow(Order order, string paymentType);
}
