using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_12
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static Dictionary<string, (List<string>, bool isUppercase)> ReadInput()
        {
            var caves = new Dictionary<string, (List<string> connectedCaves, bool isUpperCase)>();

            foreach (var line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\CaveConnections.txt"))
            {

                var currentCaves = line.Split("-");

                if (caves.ContainsKey(currentCaves[0]))
                {
                    caves[currentCaves[0]].connectedCaves.Add(currentCaves[1]);
                }
                else
                {
                    var isUppercase = false;
                    var connectedCaves = new List<string>();
                    connectedCaves.Add(currentCaves[1]);
                    if (currentCaves[0].First() < 91)
                    {
                        isUppercase = true;
                    }
                    caves.Add(currentCaves[0], (connectedCaves, isUppercase));
                }

                if (caves.ContainsKey(currentCaves[1]))
                {
                    caves[currentCaves[1]].connectedCaves.Add(currentCaves[0]);
                }
                else
                {
                    var isUppercase = false;
                    var connectedCaves = new List<string>();
                    connectedCaves.Add(currentCaves[0]);
                    if (currentCaves[1].First() < 91)
                    {
                        isUppercase = true;
                    }
                    caves.Add(currentCaves[1], (connectedCaves, isUppercase));
                }


            }

            return caves;

        }
        static void FirstStar()
        {
            var input = ReadInput();

            var paths = new List<List<string>>();

            var firstPath = new List<string>();

            paths = FindPaths(input, "start", paths, firstPath);

            Console.WriteLine(paths.Count());

            return;

        }

        static List<List<string>> FindPaths(Dictionary<string, (List<string> connectedCaves, bool isUppercase)> caves, string key, List<List<string>> paths, List<string> currentPath)
        {
            var validPathways = caves[key].connectedCaves;
            var newPath = new List<string>(currentPath);

            if (!caves[key].isUppercase && !newPath.Contains(key) || caves[key].isUppercase)
            {
                newPath.Add(key);

                if (key == "end")
                {
                    paths.Add(newPath);
                }
                else
                {
                    foreach (var connectedCave in validPathways)
                    {
                        FindPaths(caves, connectedCave, paths, newPath);
                    }
                }

            }



            return paths;
        }
        static void SecondStar()
        {
            var input = ReadInput();

            var paths = new List<List<string>>();

            var firstPath = new List<string>();

            paths = FindLongerPaths(input, "start", paths, firstPath, false);

            Console.WriteLine(paths.Count());

            return;


        }
        static List<List<string>> FindLongerPaths(Dictionary<string, (List<string> connectedCaves, bool isUppercase)> caves, string key, List<List<string>> paths, List<string> currentPath, bool hasVisitedTwice)
        {
            var validPathways = caves[key].connectedCaves;
            var newPath = new List<string>(currentPath);
            var isSecondVisit = hasVisitedTwice;

            if (!hasVisitedTwice && !caves[key].isUppercase && key != "start" || !caves[key].isUppercase && !newPath.Contains(key) || caves[key].isUppercase)
            {                
                if (!caves[key].isUppercase && newPath.Contains(key))
                {
                    isSecondVisit = true;
                }
                
                newPath.Add(key);

                if (key == "end")
                {
                    paths.Add(newPath);
                }
                else
                {
                    foreach (var connectedCave in validPathways)
                    {
                        FindLongerPaths(caves, connectedCave, paths, newPath, isSecondVisit);
                    }
                }

            }



            return paths;
        }
    }

}