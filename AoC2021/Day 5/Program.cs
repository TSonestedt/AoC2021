using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static List<int[]> ReadInput()
        {
            List<int[]> lineCoordinates = new List<int[]>();

            foreach (var line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\LineCoordinates.txt"))
            {

                var coordinates = line.Split(new string[] { " -> ", "," }, StringSplitOptions.None).Select(number =>
                {
                    return Convert.ToInt32(number);
                }).ToArray();

                lineCoordinates.Add(coordinates);

            }

            return lineCoordinates;
        }
        static void FirstStar()
        {
            List<int[]> lineCoordinates = ReadInput();

            var highestCoordinate = lineCoordinates.SelectMany(numbers =>
            {
                return numbers;

            }).Max();

            int[][] map = new int[highestCoordinate + 1].Select(x => new int[highestCoordinate + 1]).ToArray();

            foreach (var coordinates in lineCoordinates)
            {
                if (coordinates[0] == coordinates[2] || coordinates[1] == coordinates[3])
                {
                    map = DrawLine(map, coordinates);
                }
            }

            Console.WriteLine(CountHotSpots(map));



        }

        static int[][] DrawLine(int[][] map, int[] coordinates)
        {
            map[coordinates[0]][coordinates[1]]++;
            
            while (coordinates[0] != coordinates[2] || coordinates[1] != coordinates[3])
            {

                if (coordinates[0] < coordinates[2])
                {
                    coordinates[0]++;

                }
                else if (coordinates[0] > coordinates[2])
                {
                    coordinates[0]--;

                }


                if (coordinates[1] < coordinates[3])
                {
                    coordinates[1]++;

                }
                else if (coordinates[1] > coordinates[3])
                {
                    coordinates[1]--;
                    
                }

                map[coordinates[0]][coordinates[1]]++;

            }

            return map;
        }

        static int CountHotSpots(int[][] map)
        {

            return map.SelectMany(x => x).Where(number => number >= 2).Count();

        }
        static void SecondStar()
        {
            List<int[]> lineCoordinates = ReadInput();

            var highestCoordinate = lineCoordinates.SelectMany(numbers =>
            {
                return numbers;

            }).Max();

            int[][] map = new int[highestCoordinate + 1].Select(x => new int[highestCoordinate + 1]).ToArray();

            foreach (var coordinates in lineCoordinates)
            {
                    map = DrawLine(map, coordinates);

            }

            Console.WriteLine(CountHotSpots(map));
        }

    }
}