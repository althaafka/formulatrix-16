namespace DIP;

public class BadOrderService
{
    private BadEmailSender _emailSender;
    private BadFileLogger _logger;
    private BadDatabaseRepository _repository;

    public BadOrderService()
    {
        // Creating dependencies directly - tightly coupled!
        _emailSender = new BadEmailSender();
        _logger = new BadFileLogger();
        _repository = new BadDatabaseRepository();
    }

    public void ProcessOrder(string order)
    {
        // What if we want to use a different logger? Email sender? Database?
        // We'd have to modify this class!
        
        _repository.SavingOrder(order);
        _emailSender.SenderOrderConfirmation(order);
        _logger.Log($"Order processed: {order}");
    }
    
}

public class BadEmailSender
{
    public void SenderOrderConfirmation(string email)
    {
        Console.WriteLine($"Sending email to {email}");
    }
}

public class BadFileLogger
{
    public  void Log(string message)
    {
        Console.WriteLine($"Logging to file: {message}");
    }
}

public class BadDatabaseRepository
{
    public void SavingOrder(string order)
    {
        Console.WriteLine($"Saving order {order} to database");
    }
}