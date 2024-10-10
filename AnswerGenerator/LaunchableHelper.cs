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
        public async Task<Trier4.Answer> TryAsync(Func<Task<Trier4.Answer>> method, CancellationToken ct)
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
