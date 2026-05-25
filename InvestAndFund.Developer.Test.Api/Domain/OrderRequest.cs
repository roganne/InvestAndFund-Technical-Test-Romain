namespace InvestAndFund.Developer.Test.Api.Domain;

public class OrderRequest
{
    public required Order Order { get; set; }
    public required string PaymentType { get; set; }
}
