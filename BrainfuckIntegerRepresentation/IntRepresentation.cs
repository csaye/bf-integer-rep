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

        // Finds the smallest possible Brainfuck representation of the given integer
        public static string FindIntRepresentation(int intToRep, int maxIterations)
        {
            // No number below 15 is most efficiently represented through multiplication
            if (intToRep < 15)
            {
                return ToBrainfuck(intToRep);
            }

            // If most efficient to represent integer through multiplication
            if (MostEfficientAsProduct(intToRep))
            {
                List<int> factors = new List<int>();

                int[] smallestFactorPair = SmallestFactorPair(intToRep);

                factors.Add(smallestFactorPair[0]);
                factors.Add(smallestFactorPair[1]);

                bool allFactorsMostEfficient = false;

                // Loop while all factors not most efficient
                while (!allFactorsMostEfficient)
                {
                    for (int i = 0; i < factors.Count; i++)
                    {
                        // If factor most efficient as product, remove factor, add smallest factor pair, and break to loop again
                        if (MostEfficientAsProduct(factors[i]))
                        {
                            int[] factorPair = SmallestFactorPair(factors[i]);

                            factors.Add(factorPair[0]);
                            factors.Add(factorPair[1]);

                            factors.RemoveAt(i);
                            break;
                        }

                        // If no factors most efficient as product, break loop
                        allFactorsMostEfficient = true;
                    }
                }

                string representation = ToBrainfuck(factors);

                // Return shorter representation if possible
                if (maxIterations > 0 && ShorterRepresentation(intToRep, representation.Length, maxIterations) != null)
                {
                    return ShorterRepresentation(intToRep, representation.Length, maxIterations);
                }

                return representation;
            }
            // If not efficient to represent number through multiplication
            else
            {
                string representation = ToBrainfuck(intToRep);

                // Return shorter representation if possible
                if (maxIterations > 0 && ShorterRepresentation(intToRep, representation.Length, maxIterations) != null)
                {
                    return ShorterRepresentation(intToRep, representation.Length, maxIterations);
                }

                return representation;
            }
        }

        // Returns a shorter representation of the given int as an increment or decrement from adjacent integers or null if none found
        private static string ShorterRepresentation(int intToRep, int repLength, int maxIterations)
        {
            string shorterRep = null;

            string incrementedRep = FindIntRepresentation(intToRep + 1, maxIterations - 1); 
            string decrementedRep = FindIntRepresentation(intToRep - 1, maxIterations - 1);

            // If most efficient to represent integer as increment from previous integer
            if (decrementedRep.Length + 1 < repLength)
            {
                shorterRep = $"{decrementedRep}+";
            }

            // If most efficient to represent integer as decrement from next integer
            if (incrementedRep.Length + 1 < repLength)
            {
                // If shorter representation not assigned
                if (shorterRep == null)
                {
                    shorterRep = $"{incrementedRep}-";
                }

                // If value less than shorter representation
                if (incrementedRep.Length + 1 < shorterRep.Length)
                {
                    shorterRep = $"{incrementedRep}-";
                }
            }

            return shorterRep;
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
            factors.Sort();
            factors.Reverse();

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
