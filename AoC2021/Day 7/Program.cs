using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_7
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static List<int> ReadInput()
        {
            var line = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\CrabsPosition.txt");

            List<int> crabPositions = line.Split(",").Select(crab =>
            {
                return Convert.ToInt32(crab);

            }).ToList();

            return crabPositions;
        }
        static void FirstStar()
        {
            List<int> crabPositions = ReadInput();

            Console.WriteLine(FuelNeeded(crabPositions));

        }

        static int FuelNeeded(List<int> crabPositions)
        {
            var fuelNeeded = int.MaxValue;
            var bestPosition = 0;

            var highestHorizontalPosition = crabPositions.Max();

            for (var position = 0; position <= highestHorizontalPosition; position++)
            {
                var fuelSpent = 0;

                foreach (var crab in crabPositions)
                {
                    fuelSpent += Math.Abs(crab - position);
                }
                if(fuelSpent < fuelNeeded)
                {
                    fuelNeeded = fuelSpent;
                    bestPosition = position;
                }
            }
            
            Console.WriteLine(bestPosition);

            return fuelNeeded;
        }
        static void SecondStar()
        {
            List<int> crabPositions = ReadInput();

            Console.WriteLine(FuelNeededVersion2(crabPositions));

        }

        static int FuelNeededVersion2(List<int> crabPositions)
        {
            var fuelNeeded = int.MaxValue;
            var bestPosition = 0;

            var highestHorizontalPosition = crabPositions.Max();

            for (var position = 0; position <= highestHorizontalPosition; position++)
            {
                var fuelSpent = 0;

                foreach (var crab in crabPositions)
                {
                    var stepsToMove = Math.Abs(crab - position);

                    for (var step = 0; step < stepsToMove; step++)
                    {
                        fuelSpent += step + 1;
                    }
                }

                if (fuelSpent < fuelNeeded)
                {
                    fuelNeeded = fuelSpent;
                    bestPosition = position;
                }
            }

            Console.WriteLine(bestPosition);

            return fuelNeeded;
        }

    }
}