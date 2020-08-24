using System;

namespace BrainfuckIntegerRepresentation
{
    public class BrainfuckIntegerRepresentation
    {
        private static void Main(string[] args)
        {
            if (UserInputSingular())
            {
                FindInt();
            }
            else
            {
                FindIntRange();
            }
        }

        private static bool UserInputSingular()
        {
            return UserInput.SingularIntInput();
        }

        private static void FindInt()
        {
            int intToRep = UserInput.GetPositiveInt();
            string intRepresentation = FindIntRepresentation(intToRep);

            bool minimalPrint = UserInput.MinimalPrint();

            if (minimalPrint)
            {
                PrintRepresentationMinimal(intToRep, intRepresentation);
            }
            else
            {
                PrintRepresentation(intToRep, intRepresentation);
            }
        }

        private static void FindIntRange()
        {
            int[] intsToRep = UserInput.GetPositiveIntRange();

            bool minimalPrint = UserInput.MinimalPrint();

            foreach (int i in intsToRep)
            {
                string intRepresentation = FindIntRepresentation(i);

                if (minimalPrint)
                {
                    PrintRepresentationMinimal(i, intRepresentation);
                }
                else
                {
                    PrintRepresentation(i, intRepresentation);
                }
            }
        }

        private static string FindIntRepresentation(int intToRep)
        {
            return IntRepresentation.FindIntRepresentation(intToRep, 4);
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

            int cellOffset = IntRepresentation.CellOffset(intRepresentation);

            if (cellOffset == 1)
            {
                Console.WriteLine($"The result will be offset by {cellOffset} cell.");
            }
            else
            {
                Console.WriteLine($"The result will be offset by {cellOffset} cells.");
            }
        }

        private static void PrintRepresentationMinimal(int intToRep, string representation)
        {
            Console.WriteLine($"{intToRep}: {representation}");
        }
    }
}
