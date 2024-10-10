// See https://aka.ms/new-console-template for more information

using System.Reflection;
using AnswerExperiment;
using Trier4;

#region AUTOEXEC.BAT
Console.WriteLine("Hello, World!");
var answerService = new AnswerService(null);
var randomServie= new RandomService();
var cancellationToken = new CancellationToken();
#endregion AUTOEXEC.BAT


var tescik = new ServiceTierClass(randomServie,answerService);
var tescik2 = new UtilityLayerClass(tescik, answerService);
var wyswietlacz = new PresentationLayer(answerService, tescik2);
var rezultat = wyswietlacz.DisplayProductInformation(0, cancellationToken);
//Console.WriteLine(await tescik2.SpytajTescikRazOProdukt(0));
//var answerService = tescik.returnIt();

Type type = tescik2.GetType();

// Wypisujemy wszystkie pola
Console.WriteLine("Fields:");
foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
Console.WriteLine(field.Name);
}

// Wypisujemy wszystkie właściwości
Console.WriteLine("Properties:");
foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
Console.WriteLine(property.Name);
}

Console.WriteLine("Methods:");
foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
Console.WriteLine(method.Name);
}

public partial class Tescik
{
    IAnswerService _answerService;
    public Tescik(Trier4.IAnswerService answerService)
        : this()
    {
        _answerService = answerService;
    }
    public Tescik()
    {
        // Może być puste lub mieć domyślne wartości
    }
}




public class PresentationLayer
{
    IAnswerService _answerService;
    private UtilityLayerClass _utilityLayer;
    public PresentationLayer(Trier4.IAnswerService answerService, UtilityLayerClass utilityLayer)
    {
        _answerService = answerService;
        _answerService.AddYesNoDialog(new ConsoleUserDialog());
        this._utilityLayer = utilityLayer;
    }

    public async Task DisplayProductInformation(int id, CancellationToken ct)
    {
        var response = await TryAsync(() => _utilityLayer.GetOrderAndProductsData(0,ct), ct);
        if (response.IsSuccess)
        {
            Console.WriteLine(response.GetValue<string>());
        }
        else
        {
            DisplayError(response);
        }
    }

    public void DisplayError(Answer answer)
    {
        Console.ForegroundColor= ConsoleColor.Red;
        Console.Write("Error:");
        Console.ResetColor();
        Console.WriteLine(answer.Message);
    }

     



    public async Task<Trier4.Answer> TryAsync(Func< Task<Trier4.Answer>> method, CancellationToken ct)
    {
        return await Launch(method, ct);
    }

    public async Task<Trier4.Answer> Launch(Func<Task<Trier4.Answer>> method, CancellationToken ct)
{
    Console.WriteLine($"[{GetType().Name}] Launching method...");
    var operationTask = method();

    try
    {
        if (_answerService.HasTimeout)
        {
            // Use WaitAsync to add a timeout to the task
            return await operationTask.WaitAsync(_answerService.Timeout, ct);
        }

        // If there is no timeout, just await the operation
        var answer= await operationTask;
        if (!answer.IsSuccess && _answerService.HasDialog && answer.AlreadyAnswered)
        {
            var dialogResponse = _answerService.AskAsync(answer.Message, ct);
            if (await _answerService.AskAsync())
        }

                return await operationTask;
    }
    catch (TimeoutException)
    {
        Console.WriteLine($"[{GetType().Name}] Operation timed out.");
        
        // Ask the user whether to cancel or continue
        if (await _answerService.AskAsync("Operation is taking longer than expected. Do you want to continue waiting?", ct))
        {
            Console.WriteLine($"[{GetType().Name}] User chose to continue waiting.");
            // Wait for the operation to complete without timeout
            return await operationTask;
        }

        Console.WriteLine($"[{GetType().Name}] User chose to cancel the operation.");
        // Return a timed-out Answer
        return Trier4.Answer.Prepare("Operation canceled by user").TimedOut();
    }
    catch (TaskCanceledException)
    {
        Console.WriteLine($"[{GetType().Name}] Operation was canceled.");
        // Return a canceled Answer
        return Trier4.Answer.Prepare("Operation was canceled").TimedOut();
    }
}

}

public partial class ServiceTierClass : ILaunchable
{
    private RandomService _randomService;
    public ServiceTierClass(RandomService randomService)
    {
        _randomService = randomService;
    }
    public async Task<Answer> GetOrderData(int orderId, CancellationToken ct)
    {
        var response=Answer.Prepare($"GetOrderData({orderId}, {ct.ToString()})");
        return _randomService.NextBool() ?
            response.WithValue($"Order {orderId}") : 
            response.Error($"There has been an error fetching order {orderId}");
    }

    public async Task<Answer> GetProductData(int productId, CancellationToken ct)
    {
        var response= Answer.Prepare($"GetProductData({productId}, {ct.ToString()})");
        return _randomService.NextBool() ?
            response.WithValue($"Product {productId}") :
            response.Error($"There has been an error fetching product {productId}");
    }

}

public partial class UtilityLayerClass : ILaunchable
{
    private ServiceTierClass _serviceTier;

    public UtilityLayerClass(ServiceTierClass serviceTier)
    {
        this._serviceTier = serviceTier;
    }

    public async Task<Answer> GetOrderAndProductsData(int orderId,CancellationToken ct)
    {

        var response = Answer.Prepare($"GetOrderAndProductsData({orderId}, {ct.ToString()})");

        Answer resp = await TryAsync(() => _serviceTier.GetOrderData(orderId,ct), ct);
        if (!resp.IsSuccess)
        {return resp.Error($"Could not fetch order {orderId}");}
        Answer resp2 = await TryAsync(() => _serviceTier.GetProductData(orderId, ct), ct);
        return !resp2.IsSuccess ? resp.Error($"Could not fetch product {orderId}") :response.WithValue(resp.GetValue<string>()+" "+resp2.GetValue<string>());
    }
}




//public partial class TescikDwa : ILaunchable
//{
//    public async Task<Answer> SpytajTescikOneOProdukt(int id)
//    {
//        var response = Answer.Prepare("testuje tescik 1");
//        return response;
//    }
//}

//public partial class Tescik
//{
//    public Tescik(Trier4.IAnswerService answerService)
//        : this()
//    {
//        _answerService = answerService;
//    }
//}
//public partial class Tescik
//{
//    public Trier4.IAnswerService _answerService { get; private set; }
//}