// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Trier4;

Console.WriteLine("Hello, World!");
var tescik = new Tescik(new AnswerService(null));
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
public partial class Tescik : ILaunchable
{
   
}
