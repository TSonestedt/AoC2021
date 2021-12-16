using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_15
{

    class Program
    {
        static int lowestTotalRisk = Int32.MaxValue;
        static List<(List<(int y, int x)> path, int sum)> paths = new List<(List<(int y, int x)> path, int sum)>();
        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static (int risklevel, int cost)[][] ReadInput()
        {
            var chitonRiskLevel = File.ReadLines(@$"{Directory.GetCurrentDirectory()}\ChitonRiskLevel.txt")
                .Select(line => line.Select(x => (Convert.ToInt32(x.ToString()), int.MaxValue)).ToArray())
                .ToArray();

            return chitonRiskLevel;
        }
        static void FirstStar()
        {
            var input = ReadInput();

            FindSafestPath(input, 0, 0, /*new List<(int y, int x)>(),*/ 0);

            Console.WriteLine(lowestTotalRisk);

            return;
        }
        static void FindSafestPath((int risklevel, int cost)[][] riskLevels, int y, int x, /*List<(int y, int x)> path,*/ int sumOfPath)
        {

            //var currentPath = new List<(int y, int x)>();
            //foreach (var position in path)
            //{
            //    currentPath.Add(position);
            //}

            //currentPath.Add((y, x));

            if (riskLevels[y][x].cost > sumOfPath)
            {
                riskLevels[y][x].cost = sumOfPath;
            }
            else
            {
                return;
            }

            var north = (y: y - 1, x);
            var south = (y: y + 1, x);
            var west = (y, x: x - 1);
            var east = (y, x: x + 1);

            var directions = new List<(int y, int x)> { south, east, north, west };

            var validDirections = directions.Where(direction =>
                direction.x >= 0 &&
                direction.y >= 0 &&
                direction.x < riskLevels[0].Length &&
                direction.y < riskLevels.Count() /*&&
                !path.Contains(direction)*/).ToList();

            if (validDirections.Contains((riskLevels.Count() - 1, riskLevels[0].Length - 1)))
            {
                validDirections.Clear();
                validDirections.Add((riskLevels.Count() - 1, riskLevels[0].Length - 1));
            }

            if (y == riskLevels.Count() - 1 && x == riskLevels[0].Length - 1)
            {
                lowestTotalRisk = sumOfPath;

              //  paths.Add((currentPath, sumOfPath));

            }
            else
            {
                foreach (var direction in validDirections)
                {
                    FindSafestPath(riskLevels, direction.y, direction.x,/* currentPath,*/ sumOfPath + riskLevels[direction.y][direction.x].risklevel);

                }
            }


            return;

        }

        static void SecondStar()
        {
            var input = ReadInput();



        }


    }
}