using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoCWarmUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<int>();
            int counter = 0;
            foreach (string line in File.ReadLines(@"D:\Code\AoC\AoCWarmUp\AoCWarmUp\data.txt"))
            {
                numbers.Add(Int32.Parse(line));

            }

            foreach (int num1 in numbers)
            {

                for (int i = counter; i < numbers.Count; i++)
                {
                    int num2 = numbers[i];
                    if (num1 + num2 == 2020)
                    {
                        int num3 = num1 * num2;
                        Console.WriteLine(num3);
                    }
                }
                counter++;
            }
            return;
        }
    }
}
