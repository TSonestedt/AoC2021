using System;
using System.Collections.Generic;
using System.IO;

namespace Day_1
{
    class Program
    {
        static List<int> depthMeasurements = new List<int>();
        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\depthdata.txt"))
            {
                depthMeasurements.Add(Int32.Parse(line));
            }

            FirstStar();
            SecondStar();

        }

        static void FirstStar()
        {
            var numberOfTimesIncreased = 0;
            var counter = 0;

            foreach (int number in depthMeasurements)
            {

                if (counter != 0 && number > depthMeasurements[counter - 1])
                {
                    //Console.WriteLine("Increased");
                    numberOfTimesIncreased++;
                }
                counter++;
            }
            Console.WriteLine(numberOfTimesIncreased);
            return;
        }

        static void SecondStar()
        {
            var slidingWindow = new List<int>();
            var numberOfTimesIncreased = 0;
            var counter = 0;

            foreach (int number in depthMeasurements)
            {
                if (depthMeasurements.Count - counter >= 3)
                {
                    var sumOf3 = number + depthMeasurements[counter + 1] + depthMeasurements[counter + 2];
                    slidingWindow.Add(sumOf3);
                    //Console.WriteLine(sumOf3);
                }
                counter++;
            }

            var newCounter = 0;
            foreach (int number in slidingWindow)
            {
                if (newCounter != 0 && number > slidingWindow[newCounter - 1])
                {
                    numberOfTimesIncreased++;
                }
                newCounter++;
            }
            Console.WriteLine(numberOfTimesIncreased);
            return;
        }
    }
}
