using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_11
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
            var octopiEnergyLevels = File.ReadLines(@$"{Directory.GetCurrentDirectory()}\OctopiEnergyLevels.txt")
                .Select(line => line.Select(x => Convert.ToInt32(x.ToString())).ToArray())
                .ToArray();

            return octopiEnergyLevels;

        }
        static void FirstStar()
        {
            var input = ReadInput();

            var octopi = new (int energy, bool isFlashed)[input.Count()][];

            octopi = input.Select(line => line.Select(x =>
            {
                return (x, false);
            }).ToArray())
                .ToArray();

            var numberOfFlashes = 0;

            for (int step = 0; step < 100; step++)
            {
                octopi = octopi.Select(row => row.Select(octopus =>
                {
                    octopus.energy++;
                    return octopus;
                }).ToArray()).ToArray();

                for (int y = 0; y < octopi.Count(); y++)
                {
                    for (int x = 0; x < octopi[y].Length; x++)
                    {

                        if (octopi[y][x].energy > 9)
                        {
                            octopi = Flash(octopi, y, x);
                        }

                    }


                }

                numberOfFlashes += octopi.SelectMany(row => row.Where(octopus => octopus.isFlashed).ToList()).Count();

                octopi = octopi.Select(row => row.Select(octopus =>
                {
                    octopus.isFlashed = false;
                    return octopus;
                }).ToArray()).ToArray();

            }

            Console.WriteLine(numberOfFlashes);
            return;
        }

        static (int energy, bool isFlashed)[][] Flash((int energy, bool isFlashed)[][] octopi, int y, int x)
        {
            var north = (y: y - 1, x);
            var northEast = (y: y - 1, x: x + 1);
            var east = (y, x: x + 1);
            var southEast = (y: y + 1, x: x + 1);
            var south = (y: y + 1, x);
            var southWest = (y: y + 1, x: x - 1);
            var west = (y, x: x - 1);
            var northWest = (y: y - 1, x: x - 1);

            var neigbouringOctopi = new List<(int y, int x)> { north, northEast, east, southEast, south, southWest, west, northWest };

            var validNeighbours = neigbouringOctopi.Where(direction =>
                direction.y >= 0 &&
                direction.x >= 0 &&
                direction.y < octopi.Count() &&
                direction.x < octopi[y].Length &&
                octopi[direction.y][direction.x].isFlashed == false).ToList();

            octopi[y][x].isFlashed = true;

            foreach (var octopus in validNeighbours)
            {
                if (octopi[octopus.y][octopus.x].isFlashed == false)
                {
                    octopi[octopus.y][octopus.x].energy++;

                    if (octopi[octopus.y][octopus.x].energy > 9)
                    {
                        octopi = Flash(octopi, octopus.y, octopus.x);
                    }
                }


            }

            octopi[y][x].energy = 0;

            return octopi;
        }
        static void SecondStar()
        {
            var input = ReadInput();

            var octopi = new (int energy, bool isFlashed)[input.Count()][];

            octopi = input.Select(line => line.Select(x =>
            {
                return (x, false);
            }).ToArray())
                .ToArray();

            var stepCounter = 0;

            while(octopi.Length * octopi[0].Length != octopi.SelectMany(row => row.Where(octopus => octopus.energy == 0).ToList()).Count())
            {
                stepCounter++;

                octopi = octopi.Select(row => row.Select(octopus =>
                {
                    octopus.energy++;
                    return octopus;
                }).ToArray()).ToArray();

                for (int y = 0; y < octopi.Count(); y++)
                {
                    for (int x = 0; x < octopi[y].Length; x++)
                    {

                        if (octopi[y][x].energy > 9)
                        {
                            octopi = Flash(octopi, y, x);
                        }

                    }


                }

                octopi = octopi.Select(row => row.Select(octopus =>
                {
                    octopus.isFlashed = false;
                    return octopus;
                }).ToArray()).ToArray();

            }
            
            Console.WriteLine(stepCounter);
            return;
        }

    }
}