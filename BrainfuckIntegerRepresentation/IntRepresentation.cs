using System;

namespace BrainfuckIntegerRepresentation
{
    public class IntRepresentation
    {
        public static string FindIntRepresentation(int intToRep)
        {
            return null;
        }

        // Returns the smallest factor pair as the sum of both factors of given product
        public static int[] SmallestFactorPair(int product)
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
    }
}
