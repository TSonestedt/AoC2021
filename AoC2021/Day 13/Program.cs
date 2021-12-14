using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_13
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static (int[][] dottedPaper, List<string> foldInstructions) ReadInput()
        {
            var manual = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\ThermalCameraInstructions.txt");

            var foldInstructions = manual.Split("\r\n\r\n")[1].Split("\r\n").ToList();
            var dotCoordinates = manual.Split("\r\n\r\n")[0].Split("\r\n")
                .Select(x => x.Split(",")
                    .Select(number => Convert.ToInt32(number))
                    .ToArray())
                .ToList();

            var maxXValue = dotCoordinates.Select(number => number[0]).Max() + 1;
            var maxYValue = dotCoordinates.Select(number => number[1]).Max() + 1;

            var dottedPaper = new int[maxYValue][].Select(x => new int[maxXValue]).ToArray();

            foreach (var dot in dotCoordinates)
            {
                dottedPaper[dot[1]][dot[0]]++;
            }


            return (dottedPaper, foldInstructions);

        }
        static void FirstStar()
        {
            var (dottedPaper, foldInstructions) = ReadInput();

            var foldedPaper = Fold(dottedPaper, foldInstructions[0]);

            var numberOfDots = foldedPaper.SelectMany(x => x.Where(x => x > 0)).Count();

            Console.WriteLine(numberOfDots);
            return;
        }

        static int[][] Fold(int[][] dottedPaper, string foldInstruction)
        {
            var foldAxis = foldInstruction.Split("=")[0].Last();
            var foldIndex = Convert.ToInt32(foldInstruction.Split("=")[1]);
            var firstOverlappingIndex = 0;
            int[][] foldedPaper;

            if (foldAxis.Equals('y'))
            {
                foldedPaper = new int[foldIndex][].Select(x => new int[dottedPaper[0].Length]).ToArray();
                firstOverlappingIndex = foldIndex - (dottedPaper.Count() - 1 - foldIndex);

                for (int y = 0; y < dottedPaper.Count() - foldIndex; y++)
                {
                    for (int x = 0; x < dottedPaper[0].Length; x++)
                    {
                        dottedPaper[firstOverlappingIndex + y][x] += dottedPaper[dottedPaper.Count() - y - 1][x];
                    }
                }

                for (int y = 0; y < foldIndex; y++)
                {
                    for (int x = 0; x < dottedPaper[0].Length; x++)
                    {
                        foldedPaper[y][x] = dottedPaper[y][x];
                    }

                }

            }
            else
            {
                foldedPaper = new int[dottedPaper.Count()][].Select(x => new int[foldIndex]).ToArray();
                firstOverlappingIndex = foldIndex - (dottedPaper[0].Length - 1 - foldIndex);

                for (int x = 0; x < dottedPaper[0].Length - foldIndex; x++)
                {
                    for (int y = 0; y < dottedPaper.Count(); y++)
                    {
                        dottedPaper[y][firstOverlappingIndex + x] += dottedPaper[y][dottedPaper[0].Length - x - 1];
                    }
                }
                for (int x = 0; x < foldIndex; x++)
                {
                    for (int y = 0; y < dottedPaper.Count(); y++)
                    {
                        foldedPaper[y][x] = dottedPaper[y][x];
                    }

                }
            }

            return foldedPaper;
        }

        static void SecondStar()
        {
            var (dottedPaper, foldInstructions) = ReadInput();

            var foldedPaper = dottedPaper;
            foreach(var instruction in foldInstructions)
            {
                foldedPaper = Fold(foldedPaper, instruction);
            }

            var code = new char[foldedPaper.Count()][].Select(x => new char[foldedPaper[0].Length]).ToArray();

            for (int y = 0; y < foldedPaper.Count(); y++)
            {
                for (int x = 0; x < foldedPaper[0].Length; x++)
                {
                    if(foldedPaper[y][x] > 0)
                    {
                        code[y][x] = '#';
                    }
                    else
                    {
                        code[y][x] = ' ';
                    }
                }

            }
            for (int y = 0; y < code.Count(); y++)
            {
                for (int x = 0; x < code[0].Length; x++)
                { 
                Console.Write(code[y][x]);
                }
                Console.Write("\r\n");
            }

            return;
        }

    }
}