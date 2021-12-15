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

        static (string polymerTemplate, Dictionary<string, (char charToInsert, long occurances)> pairInsertions) ReadInput()
        {
            var polymerizationData = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\PolymerTemplate.txt");

            var polymerTemplate = polymerizationData.Split("\r\n\r\n")[0];
            var instructions = polymerizationData.Split("\r\n\r\n")[1].Split("\r\n").ToList();

            var pairInsertions = new Dictionary<string, (char charToInsert, long occurances)>();

            foreach (var instruction in instructions)
            {
                pairInsertions.Add(instruction.Split(" -> ")[0], (Convert.ToChar(instruction.Split(" -> ")[1]), 0));
            }

            return (polymerTemplate, pairInsertions);

        }
        static void FirstStar()
        {
            var (polymerTemplate, pairInsertions) = ReadInput();
            var polymerizedTemplate = pairInsertions;

            var polymerTemplatePairs = new List<string>();


            for (int p = 0; p < polymerTemplate.Length - 1; p++)
            {
                polymerTemplatePairs.Add(polymerTemplate.Substring(p, 2));
            }

            foreach (var pair in polymerTemplatePairs)
            {
                pairInsertions[pair] = (pairInsertions[pair].charToInsert, +1);
            }

            for(int step = 0; step < 10; step++)
            {
                polymerizedTemplate = InsertChars(polymerizedTemplate);
            }

            var occuringChars = new Dictionary<char, long>();
            occuringChars = CountCharsOccurances(polymerizedTemplate, occuringChars);

            var lastChar = polymerTemplate[polymerTemplate.Length - 1];
            occuringChars[lastChar]++;

            var mostCommonCharOccurances = occuringChars.Select(c => c.Value).Max();
            var leastCommonCharOccurances = occuringChars.Select(c => c.Value).Where(c => c > 0).Min();

            Console.WriteLine(mostCommonCharOccurances - leastCommonCharOccurances);

            return;

        }

        static Dictionary<string, (char charToinsert, long occurances)> InsertChars(Dictionary<string, (char charToinsert, long occurances)> pairInsertions)
        {
            var insertedPairs = new Dictionary<string, (char charToinsert, long occurances)>();

            foreach (var pair in pairInsertions)
            {
                insertedPairs.Add(pair.Key, (pair.Value.charToinsert, 0));
            }
            foreach (var pair in pairInsertions)
            {
                if (pair.Value.occurances > 0)
                {
                    var newPair1 = $"{pair.Key[0]}{pair.Value.charToinsert}";
                    var newPair2 = $"{pair.Value.charToinsert}{pair.Key[1]}";
                    insertedPairs[newPair1] = (insertedPairs[newPair1].charToinsert, insertedPairs[newPair1].occurances + pair.Value.occurances);
                    insertedPairs[newPair2] = (insertedPairs[newPair2].charToinsert, insertedPairs[newPair2].occurances + pair.Value.occurances);
                }
            }


            return insertedPairs;
        }

        static Dictionary<char, long> CountCharsOccurances(Dictionary<string, (char charToinsert, long occurances)> polymerizedTemplate, Dictionary<char, long> occuringChars)
        {
            foreach (var pair in polymerizedTemplate)
            {
                if(!occuringChars.TryAdd(pair.Key[0], pair.Value.occurances))
                {
                    occuringChars[pair.Key[0]] += pair.Value.occurances;
                }
              
            }


            return occuringChars;
        }

        static void SecondStar()
        {
            var (polymerTemplate, pairInsertions) = ReadInput();
            var polymerizedTemplate = pairInsertions;

            var polymerTemplatePairs = new List<string>();


            for (int p = 0; p < polymerTemplate.Length - 1; p++)
            {
                polymerTemplatePairs.Add(polymerTemplate.Substring(p, 2));
            }

            foreach (var pair in polymerTemplatePairs)
            {
                pairInsertions[pair] = (pairInsertions[pair].charToInsert, +1);
            }

            for (int step = 0; step < 40; step++)
            {
                polymerizedTemplate = InsertChars(polymerizedTemplate);
            }

            var occuringChars = new Dictionary<char, long>();
            occuringChars = CountCharsOccurances(polymerizedTemplate, occuringChars);

            var lastChar = polymerTemplate[polymerTemplate.Length - 1];
            occuringChars[lastChar]++;

            var mostCommonCharOccurances = occuringChars.Select(c => c.Value).Max();
            var leastCommonCharOccurances = occuringChars.Select(c => c.Value).Where(c => c > 0).Min();

            Console.WriteLine(mostCommonCharOccurances - leastCommonCharOccurances);

            return;
        }

    }
}