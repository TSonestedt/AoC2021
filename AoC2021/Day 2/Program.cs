using System;
using System.Collections.Generic;
using System.IO;

namespace Day_1
{
    class Program
    {
        static List<String[]> commands = new List<String[]>();
        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines(@"C:\Users\Tilian\source\repos\AoC2021\AoC2021\Day 2\CommandInputs.txt"))
            {
                commands.Add(line.Split(" "));

            }
            foreach (String[] line in commands)
            {
                var horizontalPos = 0;
                var depth = 0;
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
            return;
        }
    }

}