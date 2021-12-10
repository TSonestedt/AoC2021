using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_9
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static int[][] ReadInput()
        {
            var heightmap = File.ReadLines(@$"{Directory.GetCurrentDirectory()}\Heightmap.txt")
                .Select(line => line.Select(x => Convert.ToInt32(x.ToString())).ToArray())
                .ToArray();

            return heightmap;
        }
        static void FirstStar()
        {
            var input = ReadInput();

            var heightmap = new int[input.Count(), input[0].Length];
            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    heightmap[y, x] = input[y][x];
                }
            }

            var lowPoints = GetLowPoints(heightmap);

            var riskLevels = lowPoints.Select(x => x + 1).ToList();

            Console.WriteLine(riskLevels.Sum());
        }

        static List<int> GetLowPoints(int[,] heightmap)
        {
            var lowPoints = new List<int>();

            for (int y = 0; y < heightmap.GetLength(0); y++)
            {
                for (int x = 0; x < heightmap.GetLength(1); x++)
                {
                    var north = (y: y - 1, x);
                    var south = (y: y + 1, x);
                    var west = (y, x: x - 1);
                    var east = (y, x: x + 1);

                    var directions = new List<(int y, int x)> { north, south, west, east };

                    var validDirections = directions.Where(direction =>
                        direction.x >= 0 &&
                        direction.y >= 0 &&
                        direction.x < heightmap.GetLength(1) &&
                        direction.y < heightmap.GetLength(0)).ToList();

                    var isLowPoint = validDirections.All(d =>
                    {
                        if (heightmap[d.y, d.x] > heightmap[y, x])
                        {
                            return true;
                        }

                        return false;
                    });

                    if (isLowPoint)
                    {
                        lowPoints.Add(heightmap[y, x]);
                    }


                }
            }

            return lowPoints;
        }
        static void SecondStar()
        {
            var input = ReadInput();

            var heightmap = new (int value, int basin)[input.Count(), input[0].Length];
            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    heightmap[y, x] = (input[y][x], 0);

                }
            }

            heightmap = IdentifyBasins(heightmap);

            var heightmapFlattened = new List<(int value, int basin)>();

            for(int y = 0;y < heightmap.GetLength(0); y++)
            {
                for(int x = 0;x < heightmap.GetLength(1); x++)
                {
                    heightmapFlattened.Add(heightmap[y, x]);
                }
            }

            var numberOfBasins = heightmapFlattened.Select(x => x.basin).Max();

            var sizeOfBasins = new List<int>();

            for( int basin = 1; basin <= numberOfBasins; basin++)
            {
                var basinSize = 0;

                foreach(var point in heightmapFlattened)
                {
                    if(point.basin == basin)
                    {
                        basinSize++;
                    }
                }

                sizeOfBasins.Add(basinSize);
            }

            sizeOfBasins.Sort();

            var count = sizeOfBasins.Count();

            Console.WriteLine(sizeOfBasins[count - 1] * sizeOfBasins[count - 2] * sizeOfBasins[count - 3]);

            return;

        }

        static (int, int)[,] IdentifyBasins((int value, int basin)[,] heightmap)
        {
            var basinCounter = 0;
            for(int y = 0; y < heightmap.GetLength(0); y++)
            {
                for(int x = 0; x < heightmap.GetLength(1); x++)
                {
                    if (heightmap[y,x].basin == 0 && heightmap[y,x].value != 9)
                    {
                        basinCounter++;
                        heightmap = AssignToBasin(heightmap, y, x, basinCounter);                        

                    }
                }
            }
            return heightmap;
        }

        static (int, int)[,] AssignToBasin((int value, int basin)[,] heightmap, int y, int x, int basinID)
        {
            var north = (y: y - 1, x);
            var south = (y: y + 1, x);
            var west = (y, x: x - 1);
            var east = (y, x: x + 1);

            var directions = new List<(int y, int x)> { north, south, west, east };

            var validDirections = directions.Where(direction =>
                direction.x >= 0 &&
                direction.y >= 0 &&
                direction.x < heightmap.GetLength(1) &&
                direction.y < heightmap.GetLength(0) &&
                heightmap[direction.y, direction.x].value != 9 &&
                heightmap[direction.y, direction.x].basin == 0).ToList();

            heightmap[y, x].basin = basinID;

            foreach(var direction in validDirections)
            {
                heightmap = AssignToBasin(heightmap, direction.y, direction.x, basinID);
            }




            return heightmap;

        }

    }
}