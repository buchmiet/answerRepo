// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Trier4;

Console.WriteLine("Hello, World!");
var answerService = new AnswerService(null);
var tescik = new TescikRaz(answerService);
var tescik2 = new TescikDwa(tescik,answerService);
Console.WriteLine(await tescik2.SpytajTescikRazOProdukt(0));
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

//public partial class Tescik
//{
//    IAnswerService _answerService;
//    public Tescik(Trier4.IAnswerService answerService)
//        : this()
//    {
//        _answerService = answerService;
//    }
//    public Tescik()
//    {
//        // Może być puste lub mieć domyślne wartości
//    }
//}
public partial class TescikRaz : ILaunchable
{
    public async Task<Answer> SpytajBazeDanychOProdukt(int id)
    {
        return Answer.Prepare($"Returning product {id}");
    }
}

public partial class TescikDwa : ILaunchable
{
    private TescikRaz tescikraz;

    public TescikDwa(TescikRaz tescikraz)
    {
        this.tescikraz = tescikraz;
    }

    public async Task<Answer> SpytajTescikRazOProdukt(int id)
    {

        var response = Answer.Prepare($"pytam tescikraz o {id}");

        var resp = await TryAsync((token) => tescikraz.SpytajBazeDanychOProdukt(0), new CancellationToken());

        //);
        return resp;

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