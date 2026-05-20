public class OP
{
    public void Process(Order order, string paymentType)
    {
        if (order == null)
            throw new Exception("Order is null");

        if (order.Amount <= 0)
            throw new Exception("Invalid amount");

        if (paymentType == "Card")
        {
            Console.WriteLine("Processing card payment");
            ChargeCard(order.CardNumber, order.Amount);
        }
        else if (paymentType == "BankTransfer")
        {
            Console.WriteLine("Processing bank transfer");
            ProcessBankTransfer(order.BankAccount, order.Amount);
        }
        else
        {
            throw new Exception("Unknown payment type");
        }

        if (order.CustomerType == "VIP")
        {
            order.Amount = order.Amount * 0.8m;
        }

        SaveToDatabase(order);

        SendEmail(order.CustomerEmail, "Your order has been processed");
    }

    private void ChargeCard(string cardNumber, decimal amount)
    {
        Console.WriteLine($"Charging card {cardNumber} for {amount}");
    }

    private void ProcessBankTransfer(string account, decimal amount)
    {
        Console.WriteLine($"Processing bank transfer {account} for {amount}");
    }

    private void SaveToDatabase(Order order)
    {
        Console.WriteLine("Saving order to database");
    }

    private void SendEmail(string email, string message)
    {
        Console.WriteLine($"Sending email to {email}");
    }
}

public class Order
{
    public decimal Amount { get; set; }
    public string CustomerType { get; set; }
    public string CustomerEmail { get; set; }
    public string CardNumber { get; set; }
    public string BankAccount { get; set; }
}