namespace InvestAndFund.Developer.Test.Api.Domain.Enums;

public static class PaymentTypeEnum
{
    public const string Card = "Card";
    public const string BankTransfer = "BankTransfer";

    public static readonly HashSet<string> ValidPaymentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        Card,
        BankTransfer
    };
}
