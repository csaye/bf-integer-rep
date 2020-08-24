using System;

namespace BrainfuckIntegerRepresentation
{
    class BrainfuckIntegerRepresentation
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
            return null;
        }

        private static void PrintRepresentation(int intToRep, string intRepresentation)
        {
            Console.WriteLine($"The smallest representation of {intToRep} in Brainfuck is:");
            Console.WriteLine(intRepresentation);
        }
    }
}
