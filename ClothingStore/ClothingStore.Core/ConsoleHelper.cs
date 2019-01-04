using System;

namespace ClothingStore.Core
{
    public static class ConsoleHelper
    {
        public static int? ReadInteger()
        {
            int result;
            string input = Console.ReadLine().Trim();

            // Parse the provided input into integer
            if (!string.IsNullOrWhiteSpace(input)
                && int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
