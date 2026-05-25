using InvestAndFund.Developer.Test.Api.Application.Validation;
using InvestAndFund.Developer.Test.Api.Domain;

namespace InvestAndFund.Developer.Test.Api.Filters;

public class ValidationFilter(IOrderValidator validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.FirstOrDefault(arg => arg is OrderRequest) as OrderRequest;

        if (request is null)
        {
            return Results.BadRequest(new { Error = "Request body is required" });
        }

        if (!validator.TryValidate(request.Order, request.PaymentType, out var errorMessage))
        {
            return Results.BadRequest(new { Error = errorMessage });
        }

        return await next(context);
    }
}
