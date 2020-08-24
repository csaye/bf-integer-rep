using System;

namespace BrainfuckIntegerRepresentation
{
    class BrainfuckIntegerRepresentation
    {
        private static int numToRep;

        private static void Main(string[] args)
        {
            GetUserInput();
        }

        // Gets a positive integer from the user and assigns its value to numToRep
        private static void GetUserInput()
        {
            bool inputParsed = false;

            while (!inputParsed)
            {
                Console.WriteLine("Please enter a positive integer.");
                string userInput = Console.ReadLine();

                inputParsed = TryParse(userInput);
            }
        }

        // Returns whether parsing of given input was successful
        private static bool TryParse(string userInput)
        {
            if (int.TryParse(userInput, out numToRep))
            {
                // Positive integer
                if (numToRep > 0)
                {
                    return true;
                }
                // Negative integer
                else
                {
                    return false;
                }
            }
            // Not integer
            else
            {
                return false;
            }
        }
    }
}
