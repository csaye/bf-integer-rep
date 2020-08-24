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
            return FindIntRepresentation(intToRep, true, 0, 0);
        }

        // Finds the smallest possible Brainfuck representation of given integer
        private static string FindIntRepresentation(int intToRep, bool beginChecks, int leftChecks, int rightChecks)
        {
            // No number below 15 is most efficiently represented through multiplication
            if (intToRep < 15)
            {
                return CharString('+', intToRep);
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
                    bool checkFactorsAgain = false;

                    for (int i = 0; i < factors.Count; i++)
                    {
                        // If factor most efficient as product, remove factor, add smallest factor pair, and break to loop again
                        if (MostEfficientAsProduct(factors[i]))
                        {
                            int[] factorPair = SmallestFactorPair(factors[i]);

                            factors.Add(factorPair[0]);
                            factors.Add(factorPair[1]);

                            factors.RemoveAt(i);
                            checkFactorsAgain = true;
                            break;
                        }
                    }

                    // If no factors most efficient as product, break loop
                    if (!checkFactorsAgain)
                    {
                        allFactorsMostEfficient = true;
                    }
                }

                string representation = ToBrainfuck(factors);

                // Return shorter representation if possible
                if (beginChecks || leftChecks > 0 || rightChecks > 0)
                {
                    string shorterRep;

                    if (beginChecks)
                    {
                        int checkDistance = CheckDistance(intToRep, representation.Length);
                        shorterRep = ShorterRepresentation(intToRep, representation.Length, checkDistance, checkDistance);
                    }
                    else
                    {
                        shorterRep = ShorterRepresentation(intToRep, representation.Length, leftChecks, rightChecks);
                    }

                    if (shorterRep != null)
                    {
                        return shorterRep;
                    }
                }

                return representation;
            }
            // If not efficient to represent number through multiplication
            else
            {
                string representation = CharString('+', intToRep);

                // Return shorter representation if possible
                if (beginChecks || leftChecks > 0 || rightChecks > 0)
                {
                    string shorterRep;

                    if (beginChecks)
                    {
                        int checkDistance = CheckDistance(intToRep, representation.Length);
                        shorterRep = ShorterRepresentation(intToRep, representation.Length, checkDistance, checkDistance);
                    }
                    else
                    {
                        shorterRep = ShorterRepresentation(intToRep, representation.Length, leftChecks, rightChecks);
                    }

                    if (shorterRep != null)
                    {
                        return shorterRep;
                    }
                }

                return representation;
            }
        }

        // Returns the distance necessary to check in each direction in order to ensure optimal representation length
        private static int CheckDistance(int intToRep, int repLength)
        {
            return repLength - ClosestPowerOfTwoLength(intToRep);
        }

        // Returns the representation length of the closest power of two
        private static int ClosestPowerOfTwoLength(int num)
        {
            int closestPowerOfTwo = 1;

            while (closestPowerOfTwo * 2 < num)
            {
                closestPowerOfTwo *= 2;
            }

            return FindIntRepresentation(closestPowerOfTwo, false, 0, 0).Length;
        }

        // Returns a shorter representation of the given int as an increment or decrement from adjacent integers or null if none found
        private static string ShorterRepresentation(int intToRep, int repLength, int leftChecks, int rightChecks)
        {
            string shorterRep = null;

            string decrementedRep = "";
            string incrementedRep = "";

            if (leftChecks > 0)
            {
                decrementedRep = FindIntRepresentation(intToRep - 1, false, leftChecks - 1, 0);
            }
            if (rightChecks > 0)
            {
                incrementedRep = FindIntRepresentation(intToRep + 1, false, 0, rightChecks - 1);
            }

            // If most efficient to represent integer as increment from previous integer
            if (leftChecks > 0 && decrementedRep.Length + CellOffset(decrementedRep) + 1 < repLength)
            {
                int cellOffset = CellOffset(decrementedRep);
                string cellOffsetString = CharString('>', cellOffset);
                shorterRep = $"{decrementedRep}{cellOffsetString}+";
            }

            // If most efficient to represent integer as decrement from next integer
            if (rightChecks > 0 && incrementedRep.Length + CellOffset(incrementedRep) + 1 < repLength)
            {
                // If shorter representation not assigned
                if (shorterRep == null)
                {
                    int cellOffset = CellOffset(incrementedRep);
                    string cellOffsetString = CharString('>', cellOffset);
                    shorterRep = $"{incrementedRep}{cellOffsetString}+";
                }

                // If value less than shorter representation
                if (incrementedRep.Length + CellOffset(incrementedRep) + 1 < shorterRep.Length)
                {
                    int cellOffset = CellOffset(incrementedRep);
                    string cellOffsetString = CharString('>', cellOffset);
                    shorterRep = $"{incrementedRep}{cellOffsetString}+";
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

        // Returns the given char repeated for length number of times
        private static string CharString(char ch, int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(ch);
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
            sb.Append(CharString('+', factors[0]));

            // Add rest of factors
            for (int i = 1; i < factors.Count; i++)
            {
                sb.Append(openingSegment);
                sb.Append(CharString('+', factors[i]));

                numClosingSegments++;
            }

            for (int i = 0; i < numClosingSegments; i++)
            {
                sb.Append(closingSegment);
            }

            return sb.ToString();
        }

        // Returns the cell offset of given representation as a count of the number of data point shifts
        public static int CellOffset(string representation)
        {
            int endingIndex = representation.LastIndexOf(']');

            // Return zero if no data shifts
            if (endingIndex == -1)
            {
                return 0;
            }

            string beforeEnding = representation.Substring(0, endingIndex);
            string afterEnding = representation.Substring(endingIndex);

            int shiftsBeforeEnding = beforeEnding.Split('>').Length - 1;
            int shiftsAfterEnding = afterEnding.Split('>').Length - 1;

            // Return the number of shifts after multiplication subtracted from the number of shifts during multiplication
            return shiftsBeforeEnding - shiftsAfterEnding;
        }
    }
}
