using System;

namespace BrainfuckIntegerRepresentation
{
    public class BrainfuckIntegerRepresentation
    {
        private static void Main(string[] args)
        {
            int intToRep = GetUserInput();
            string intRepresentation = FindIntRepresentation(intToRep);
            PrintRepresentation(intToRep, intRepresentation);
        }

        private static int GetUserInput()
        {
            return UserInput.GetPositiveInt();
        }

        private static string FindIntRepresentation(int intToRep)
        {
            return IntRepresentation.FindIntRepresentation(intToRep, true);
        }

        private static void PrintRepresentation(int intToRep, string intRepresentation)
        {
            if (intRepresentation.Length == 1)
            {
                Console.WriteLine($"The smallest representation of {intToRep} in Brainfuck is {intRepresentation.Length} character:");
            }
            else
            {
                Console.WriteLine($"The smallest representation of {intToRep} in Brainfuck is {intRepresentation.Length} characters:");
            }

            Console.WriteLine(intRepresentation);

            // Find cell offset by counting number of data pointer shifts in integer representation
            int cellOffset = intRepresentation.Split('>').Length - 1;

            if (cellOffset == 1)
            {
                Console.WriteLine($"The result will be offset by {cellOffset} cell.");
            }
            else
            {
                Console.WriteLine($"The result will be offset by {cellOffset} cells.");
            }
        }
    }
}
