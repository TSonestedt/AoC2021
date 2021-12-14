using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_14
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static (string polymerTemplate, Dictionary<string, char> pairInsertions) ReadInput()
        {
            var polymerizationData = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\PolymerTemplate.txt");

            var polymerTemplate = polymerizationData.Split("\r\n\r\n")[0];
            var instructions = polymerizationData.Split("\r\n\r\n")[1].Split("\r\n").ToList();

            var pairInsertions = new Dictionary<string, char>();

            foreach (var instruction in instructions)
            {
                pairInsertions.Add(instruction.Split(" -> ")[0], Convert.ToChar(instruction.Split(" -> ")[1]));
            }

            return (polymerTemplate, pairInsertions);

        }
        static void FirstStar()
        {
            var (polymerTermplate, pairInsertions) = ReadInput();
            string polymerizedTemplate = polymerTermplate;

            for (int step = 0; step < 10; step++)
            {
                polymerizedTemplate = InsertChar(polymerizedTemplate, pairInsertions);
            }

            var occuringChars = new Dictionary<char, int>();
            occuringChars = CountCharsOccurances(polymerizedTemplate, occuringChars);

            var mostCommonCharOccurances = occuringChars.Select(c => c.Value).Max();
            var leastCommonCharOccurances = occuringChars.Select(c => c.Value).Min();

            Console.WriteLine(mostCommonCharOccurances - leastCommonCharOccurances);

            return;

        }

        static string InsertChar(string polymerTemplate, Dictionary<string, char> pairInsertions)
        {
            var polymerTemplatePairs = new List<string>();
            var charsToInsert = new List<char>();

            for (int p = 0; p < polymerTemplate.Length - 1; p++)
            {
                polymerTemplatePairs.Add(polymerTemplate.Substring(p, 2));
            }

            foreach (var pair in polymerTemplatePairs)
            {
                charsToInsert.Add(pairInsertions[pair]);
            }

            var indexToInsertAt = 1;

            for (int i = 0; i < charsToInsert.Count; i++)
            {
                polymerTemplate = polymerTemplate.Insert(indexToInsertAt, Convert.ToString(charsToInsert[i]));
                indexToInsertAt += 2;
            }


            return polymerTemplate;
        }

        static Dictionary<char, int> CountCharsOccurances(string polymerizedTemplate, Dictionary<char, int> occuringChars)
        {
            for (int i = 0; i < polymerizedTemplate.Length; i++)
            {
                if (occuringChars.ContainsKey(polymerizedTemplate[i]))
                {
                    occuringChars[polymerizedTemplate[i]]++;
                }
                else
                {
                    occuringChars.Add(polymerizedTemplate[i], 1);
                }
            }


            return occuringChars;
        }

        static void SecondStar()
        {

        }

    }
}