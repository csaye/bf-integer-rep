using System;

namespace BrainfuckIntegerRepresentation
{
    public class UserInput
    {
        private static int postiveInteger;

        private static int rangeLowerBound;
        private static int rangeUpperBound;

        // Returns true if the user requests input of a singular integer and negative if range requested
        public static bool SingularIntInput()
        {
            // Continue asking user for input while invalid strings entered
            while (true)
            {
                Console.WriteLine("Enter \"single\" for single int representation or \"range\" for int range representation.");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "single")
                {
                    return true;
                }
                else if (userInput.ToLower() == "range")
                {
                    return false;
                }
            }
        }

        // Gets and returns positive integer from the user
        public static int GetPositiveInt()
        {
            bool inputParsed = false;

            // Continue asking user for input while invalid strings entered
            while (!inputParsed)
            {
                Console.WriteLine("Enter a positive integer (number form).");
                string userInput = Console.ReadLine();

                inputParsed = TryParsePositiveInt(userInput);
            }

            return postiveInteger;
        }

        // Returns whether parsing of given input was successful
        private static bool TryParsePositiveInt(string userInput)
        {
            // Not integer
            if (!int.TryParse(userInput, out postiveInteger))
            {
                return false;
            }

            // Positive integer
            if (postiveInteger < 0)
            {
                return false;
            }

            return true;
        }

        // Gets and returns positive integer from the user
        public static int[] GetPositiveIntRange()
        {
            bool inputParsed = false;

            // Continue asking user for input while invalid strings entered
            while (!inputParsed)
            {
                Console.WriteLine("Enter two positive integers (number form) separated by a space to represent the min (inclusive) and max (inclusive) of the range, respectively.");
                string userInput = Console.ReadLine();

                inputParsed = TryParsePositiveIntRange(userInput);
            }

            int[] range = new int[(rangeUpperBound + 1) - rangeLowerBound];

            for (int i = 0; i < range.Length; i++)
            {
                range[i] = rangeLowerBound + i;
            }

            return range;
        }

        // Returns whether parsing of given input was successful
        private static bool TryParsePositiveIntRange(string userInput)
        {
            string[] userInputArray = userInput.Split(' ');

            // Incorrect number of entries
            if (userInputArray.Length != 2)
            {
                return false;
            }

            // Not integer
            if (!int.TryParse(userInputArray[0], out rangeLowerBound))
            {
                return false;
            }

            // Not integer
            if (!int.TryParse(userInputArray[1], out rangeUpperBound))
            {
                return false;
            }

            // Negative integer
            if (rangeLowerBound < 0 || rangeUpperBound < 0)
            {
                return false;
            }

            // Incorrect range
            if (rangeLowerBound > rangeUpperBound)
            {
                return false;
            }

            return true;
        }
    }
}
