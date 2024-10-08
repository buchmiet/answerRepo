// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Trier4;

Console.WriteLine("Hello, World!");
var tescik = new TescikRaz(new AnswerService(null));
//var answerService = tescik.returnIt();

Type type = tescik.GetType();

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

public partial class TescikRaz
{
    public IAnswerService _answerService { get; private set; }
}

public partial class TescikRaz
{
    //public TescikRaz(Trier4.IAnswerService answerService)
    //{
    //    _answerService = answerService;
    //}
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