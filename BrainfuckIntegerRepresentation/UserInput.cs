using System;

namespace BrainfuckIntegerRepresentation
{
    public class UserInput
    {
        private static int postiveInteger;

        // Gets and returns positive integer from the user
        public static int GetPositiveInt()
        {
            bool inputParsed = false;

            // Continue asking user for input while invalid strings entered
            while (!inputParsed)
            {
                Console.WriteLine("Please enter a positive integer.");
                string userInput = Console.ReadLine();

                inputParsed = TryParsePositiveInt(userInput);
            }

            return postiveInteger;
        }

        // Returns whether parsing of given input was successful
        private static bool TryParsePositiveInt(string userInput)
        {
            if (int.TryParse(userInput, out postiveInteger))
            {
                // Positive integer
                if (postiveInteger > 0)
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
