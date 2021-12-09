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
            var heightmap = ReadInput();
        }
        static void SecondStar()
        {
           
        }

    }
}