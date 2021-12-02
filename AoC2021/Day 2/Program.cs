using System;
using System.Collections.Generic;
using System.IO;

namespace Day_2
{
    class Program
    {
        static List<String[]> commands = new List<String[]>();
        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\CommandInputs.txt"))
            {
                commands.Add(line.Split(" "));

            }

            FirstStar();
            SecondStar();

            return;
        }
        static void FirstStar()
        {
            var horizontalPos = 0;
            var depth = 0;

            foreach (String[] line in commands)
            {

                var distance = Int32.Parse(line[1]);

                switch (line[0])
                {
                    case "forward":
                        horizontalPos += distance;
                        break;
                    case "down":
                        depth += distance;
                        break;
                    case "up":
                        depth -= distance;
                        break;
                }

            }

            Console.WriteLine($"Horisontal position {horizontalPos}, depth {depth}");
            Console.WriteLine(horizontalPos * depth);
            return;
        }

        static void SecondStar()
        {
            var horizontalPos = 0;
            var depth = 0;
            var aim = 0;

            foreach (String[] line in commands)
            {

                var value = Int32.Parse(line[1]);

                switch (line[0])
                {
                    case "forward":
                        horizontalPos += value;
                        depth += value * aim;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }

            }

            Console.WriteLine($"Horisontal position {horizontalPos}, depth {depth}");
            Console.WriteLine(horizontalPos * depth);
            return;
        }
    }

}