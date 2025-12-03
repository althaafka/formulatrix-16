namespace DIP;

public interface IEmailSender
{
    void SendOrderConfirmation(string email);
}

public interface ILogger
{
    void Log(string message);
}

public interface IOrderRepository
{
    void SaveOrder(string order);
}

public class SmtpEmailSender : IEmailSender
{
    public void SendOrderConfirmation(string email)
    {
        Console.WriteLine($"Sending email via SMTP to {email}");
    }
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Writing to file: {message}");
    }
}

public class SqlOrderRepository : IOrderRepository
{
    public void SaveOrder(string order)
    {
        Console.WriteLine($"Saving order {order} to SQL database");
    }
}

public class OrderService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;
    private readonly IOrderRepository _repository;

    public OrderService(
        IEmailSender emailSender,
        ILogger logger,
        IOrderRepository repository)
    {
        _emailSender = emailSender;
        _logger = logger;
        _repository = repository;
    }

    public void ProcessOrder(string order)
    {
        _repository.SaveOrder(order);
        _emailSender.SendOrderConfirmation(order);
        _logger.Log($"Order processed: {order}");
    }
}
