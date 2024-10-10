using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnswerExperiment
{
    public class RandomService
    {
        private readonly Random _random;

        public RandomService(int? seed = null)
        {
            // Jeśli seed nie jest przekazany, używamy domyślnego konstruktora Random, który
            // inicjalizuje się na podstawie czasu systemowego, aby generować różne wartości
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public bool NextBool()
        {
            return _random.NextDouble() >= 0.5;
        }
    }

}
