namespace CoinsChange
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        static List<int> typeOfCoins;
        static string input;

        /// <summary>
        /// Main structure of the program
        /// </summary>
        public static void Main()
        {
            TypeOfCoinsReceiver();

            while (true)
            {
                EnterAmount();

                decimal amount = ParseInputedAmount();

                if (amount < 0)
                {
                    Console.WriteLine(GlobalConstants.POSITIVE_AMOUNT_MESSAGE);
                    continue;
                }

                decimal roundedAmount = RoundAmount(amount);
                decimal centsToReturn = ConvertAmountToCents(roundedAmount);
                var result = MinCoinsCalculation(typeOfCoins, (int)centsToReturn);

                Console.WriteLine(GlobalConstants.RESULT_MESSAGE + result);
            }
        }

        /// <summary>
        /// Read for available coins from external file
        /// </summary>
        private static void TypeOfCoinsReceiver()
        {
            using (StreamReader reader = new StreamReader(ConnectionPath.PATH))
            {
                string readedData = reader.ReadToEnd();

                typeOfCoins = readedData
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(unit =>
                    {
                        int parsedUnit;
                        return int.TryParse(unit, out parsedUnit) ? parsedUnit : -1;
                    })
                    .Where(parsedUnit => parsedUnit > 0)
                    .ToList();
            }
        }

        /// <summary>
        /// Read needed cash for return to user
        /// </summary>
        private static void EnterAmount()
        {
            Console.WriteLine(GlobalConstants.INPUT_MESSAGE);

            input = Console.ReadLine();
            if (input == GlobalConstants.EMPTY_INSERT)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Parse input to proper type for money calculation
        /// </summary>
        /// <returns>decimal</returns>
        private static decimal ParseInputedAmount()
        {
            var isNumber = decimal.TryParse(input, out decimal amount);
            if (!isNumber)
            {
                Console.WriteLine(GlobalConstants.NUMBER_AMOUNT_MESSAGE);
            }

            return amount;
        }

        /// <summary>
        /// Rounds the data to the second character after the decimal point for correctness of data
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>decimal</returns>
        private static decimal RoundAmount(decimal amount)
        {
            return decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Convert amount to cents for correct algorithm calculation
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>decimal</returns>
        private static decimal ConvertAmountToCents(decimal amount)
        {
            return amount * 100;
        }

        /// <summary>
        /// Dynamic programming algorithm for calculate the minimum needed count of coins to return
        /// </summary>
        /// <param name="typeOfCoins"></param>
        /// <param name="CentsToReturn"></param>
        /// <returns>int</returns>
        public static int MinCoinsCalculation(List<int> typeOfCoins, int CentsToReturn)
        {
            var typeOfCoinsCount = typeOfCoins.Count();

            int[] tempArray = new int[CentsToReturn + 1];

            tempArray[0] = 0;

            for (int i = 1; i <= CentsToReturn; i++)
            {
                tempArray[i] = int.MaxValue;
            }

            for (int i = 1; i <= CentsToReturn; i++)
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

            return tempArray[CentsToReturn];
        }
    }
}
