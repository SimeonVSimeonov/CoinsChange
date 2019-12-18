using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoinsChange
{
    class Program
    {
        static List<int> typeOfCoins;
        static string input;

        static void Main()
        {

            using (StreamReader reader = new StreamReader("../../../Coins.csv"))
            {
                string readedData = reader.ReadToEnd();

                typeOfCoins = readedData
                    .Split(';', System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
            }

            while (true)
            {
                EnterAmount();

                if (input == "")
                {
                    Environment.Exit(0);
                }

                decimal.TryParse(input, out decimal amount);
                decimal cents = amount * 100;
                var typeOfCoinsCount = typeOfCoins.Count();

                var result = MinCoins(typeOfCoins, typeOfCoinsCount, (int)cents);

                Console.WriteLine($"Minimum coins: {result}");

            }

        }

        private static int MinCoins(List<int> typeOfCoins, int typeOfCoinsCount, int cents)
        {
            int[] tempArray = new int[cents + 1];

            tempArray[0] = 0;

            for (int i = 1; i <= cents; i++)
            {
                tempArray[i] = int.MaxValue;
            }

            for (int i = 1; i <= cents; i++)
            {

                for (int j = 0; j < typeOfCoinsCount; j++)
                {
                    if (typeOfCoins[j] <= i)
                    {
                        int tempResult = tempArray[i - typeOfCoins[j]];

                        if (tempResult != int.MaxValue &&
                            tempResult + 1 < tempArray[i])
                        {
                            tempArray[i] = tempResult + 1;
                        }
                    }
                }
            }

            return tempArray[cents];
        }

        private static void EnterAmount()
        {
            Console.WriteLine("Enter an amount: ");
            input = Console.ReadLine();
        }
    }
}
