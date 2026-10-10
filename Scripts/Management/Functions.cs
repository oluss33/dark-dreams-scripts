using System;
using Random = UnityEngine.Random;

namespace Scripts
{
    public static class Functions
    {
        public static int RandomNumberExcept(int min, int max, int except)
        {
            
            if (max - min <= 1 && min == except)
            {
                throw new ArgumentException("No valid numbers to choose from");
            }
            
            int number;
            do
            {
                int newNumber = Random.Range(min, max);
                number = newNumber;
            } while (number == except && max - min > 1);

            return number;
        }
    }
}