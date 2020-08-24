using System;
using System.Collections.Generic;
using System.Text;

namespace BrainfuckIntegerRepresentation
{
    public class IntRepresentation
    {
        // The number of chars required to perform cell multiplication in Brainfuck: [><-]
        private const int numMultiplicationChars = 5;

        // Opening and closing segments for performing cell multiplication in Brainfuck
        private const string openingSegment = "[>";
        private const string closingSegment = "<-]";

        public static string FindIntRepresentation(int intToRep)
        {
            // If more efficient to represent integer through multiplication
            if (MostEfficientAsProduct(intToRep))
            {
                List<int> factors = new List<int>();

                int[] smallestFactorPair = SmallestFactorPair(intToRep);

                factors.Add(smallestFactorPair[0]);
                factors.Add(smallestFactorPair[1]);

                bool allFactorsMostEfficient = false;

                while (!allFactorsMostEfficient)
                {

                }

                return ToBrainfuck(factors);
            }
            else
            {
                return ToBrainfuck(intToRep);
            }
        }

        // Returns whether the given integer is most efficiently represented through multiplication
        private static bool MostEfficientAsProduct(int intToRep)
        {
            int[] smallestFactorPair = SmallestFactorPair(intToRep);

            return Size(smallestFactorPair) + numMultiplicationChars < intToRep;
        }

        // Returns the smallest factor pair as the sum of both factors of given product
        private static int[] SmallestFactorPair(int product)
        {
            // Start with square root of product
            int currentNum = (int)Math.Sqrt(product);

            // If square number
            if (currentNum * currentNum == product)
            {
                return new int[2] {currentNum, currentNum};
            }

            // Move outwards from square root of product
            while (currentNum > 1)
            {
                // If factor of product
                if (product % currentNum == 0)
                {
                    return new int[2] {currentNum, product / currentNum};
                }

                currentNum--;
            }

            // If prime number
            return new int[2] {1, product};
        }

        // Returns the size of the given integer array as the sum of all of its values
        private static int Size(int[] array)
        {
            int size = 0;

            foreach (int i in array)
            {
                size += i;
            }

            return size;
        }

        // Returns the given integer as a series of plus signs
        private static string ToBrainfuck(int intToRep)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < intToRep; i++)
            {
                sb.Append('+');
            }

            return sb.ToString();
        }

        // Returns the Brainfuck representation of the multiplication of all given factors
        private static string ToBrainfuck(List<int> factors)
        {
            StringBuilder sb = new StringBuilder();

            int numClosingSegments = 0;

            // Add first factor
            sb.Append(ToBrainfuck(factors[0]));

            // Add rest of factors
            for (int i = 1; i < factors.Count; i++)
            {
                sb.Append(openingSegment);
                sb.Append(ToBrainfuck(factors[i]));

                numClosingSegments++;
            }

            for (int i = 0; i < numClosingSegments; i++)
            {
                sb.Append(closingSegment);
            }

            return sb.ToString();
        }
    }
}
