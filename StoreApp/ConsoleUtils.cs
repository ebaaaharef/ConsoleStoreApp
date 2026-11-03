using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp
{
    internal class ConsoleUtils
    {
        public static string ReadNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty, Please try again");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public static int ReadInt(string prompt, int minValue = 0)
        {
            int acceptedValue;
            Console.WriteLine(prompt);
            while (!int.TryParse(Console.ReadLine(), out acceptedValue) || acceptedValue < minValue)
            {
                Console.WriteLine($"Invalid input. Please enter an integer greater than or equal to {minValue}.");
            }
            return acceptedValue;
        }

        public static decimal ReadDecimal(string prompt, decimal minValue = 0.01m)
        {
            decimal acceptedValue;
            Console.WriteLine(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out acceptedValue) || acceptedValue < minValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number greater than or equal to {minValue}.");
            }
            return acceptedValue;
        }

        public static string ReadPhoneNumber(string prompt)
        {
            string phone;
            do
            {
                phone = ReadNonEmptyString(prompt);

                if (phone.All(char.IsDigit) && phone.StartsWith("09") && phone.Length == 10)
                {
                    return phone;
                }

                Console.WriteLine("Invalid phone number! Must start with '09' and contain exactly 10 digits.");
            } while (true);
        }

        public static bool ReadYesNo(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt + " (y/n)");
                input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                if (input == "y" || input == "yes")
                {
                    return true;
                }
                else if (input == "n" || input == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                }
            } while (true);
        }
    }
}
