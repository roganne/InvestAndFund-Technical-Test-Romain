namespace InvestAndFund.Developer.Test.Api.Domain;

public class Order
{
    public decimal Amount { get; set; }
    public required string CustomerType { get; set; }
    public required string CustomerEmail { get; set; }
    public required string CardNumber { get; set; }
    public required string BankAccount { get; set; }
}
