using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trier4;

namespace AnswerExperiment
{
    public class ConsoleUserDialog : IUserDialog
    {
        public async Task<bool> YesNoAsync(string errorMessage, CancellationToken ct)
        {
            Console.WriteLine($"there has been an error while {errorMessage}, press (y/n) to continue");

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Operation cancelled.");
                    return false; // Zwróć false w przypadku anulowania
                }

                // Odczyt z konsoli powinien być uruchomiony w osobnym zadaniu
                Task<string> inputTask = Task.Run(() => Console.ReadLine(), ct);

                try
                {
                    string input = await inputTask;

                    if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please type 'y' or 'n'.");
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation cancelled.");
                    return false; // Zwróć false w przypadku anulowania
                }
            }
        }
    }
}
