namespace ConsoleApp;

internal static class ConsoleUtilities
{
    public static int GetIntegerInput(int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("Invalid input. Integer was expected.");
                continue;
            }

            if(number < min)
            {
                Console.WriteLine($"Input must be greater or equal to {min}");
                continue;
            }

            if(number > max)
            {
                Console.WriteLine($"Input must be smaller than {max}");
                continue;
            }

            return number;
        }
    }

    public static decimal GetDecimalInput(decimal min = int.MinValue, decimal max = int.MaxValue)
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (!decimal.TryParse(input, out decimal number))
            {
                Console.WriteLine("Invalid input. Integer was expected.");
                continue;
            }

            if (number < min)
            {
                Console.WriteLine($"Input must be greater or equal to {min}");
                continue;
            }

            if (number > max)
            {
                Console.WriteLine($"Input must be smaller than {max}");
                continue;
            }

            return number;
        }
    }
}
