using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Trier4;

namespace AnswerGenerator
{
    public class LaunchableHelper
    {
        public async Task<Trier4.Answer> TryAsync(Func<CancellationToken, Task<Trier4.Answer>> method, CancellationToken ct)
        {
            return await Launch(method, ct);
        }

        public async Task<Trier4.Answer> Launch(Func<CancellationToken, Task<Trier4.Answer>> method, CancellationToken ct)
        {
            Console.WriteLine($"[{GetType().Name}] Launching method...");
            var operationTask = method(ct);

            Task completedTask;
            if (!_answerService.HasTimeout)
            {
                return await operationTask;
            }
            else
            {
                var timeoutTask = Task.Delay(_answerService.Timeout, ct);
                completedTask = await Task.WhenAny(operationTask, timeoutTask);
            }


            if (completedTask == operationTask)
            {
                Console.WriteLine($"[{GetType().Name}] Operation completed before timeout.");
                return await operationTask;
            }

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
    }

}
