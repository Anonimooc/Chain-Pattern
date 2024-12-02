public abstract class RequestHandler
{
    protected RequestHandler _nextHandler;

    // Устанавливаем следующий обработчик в цепочке
    public void SetNextHandler(RequestHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    // Метод обработки запроса
    public abstract void HandleRequest(Request request);
}

public class LoginHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == RequestType.Login)
        {
            Console.WriteLine("Handling login request.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

public class PaymentHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == RequestType.Payment)
        {
            Console.WriteLine("Handling payment request.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

public class SupportHandler : RequestHandler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == RequestType.Support)
        {
            Console.WriteLine("Handling support request.");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleRequest(request);
        }
    }
}

public class Request
{
    public RequestType Type { get; set; }
    public string Content { get; set; }

    public Request(RequestType type, string content)
    {
        Type = type;
        Content = content;
    }
}

public enum RequestType
{
    Login,
    Payment,
    Support
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем обработчики
        RequestHandler loginHandler = new LoginHandler();
        RequestHandler paymentHandler = new PaymentHandler();
        RequestHandler supportHandler = new SupportHandler();

        // Настроим цепочку: loginHandler -> paymentHandler -> supportHandler
        loginHandler.SetNextHandler(paymentHandler);
        paymentHandler.SetNextHandler(supportHandler);

        // Создаем запросы
        Request loginRequest = new Request(RequestType.Login, "User login request");
        Request paymentRequest = new Request(RequestType.Payment, "Payment processing request");
        Request supportRequest = new Request(RequestType.Support, "Customer support request");

        // Отправляем запросы по цепочке
        Console.WriteLine("Sending login request...");
        loginHandler.HandleRequest(loginRequest);

        Console.WriteLine("\nSending payment request...");
        loginHandler.HandleRequest(paymentRequest);

        Console.WriteLine("\nSending support request...");
        loginHandler.HandleRequest(supportRequest);
    }
}
