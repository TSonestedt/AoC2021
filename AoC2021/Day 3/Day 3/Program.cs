using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_1
{
    class Program
    {
        static List<string> diagnosticReport = new List<string>();
        static void Main(string[] args)
        {
            foreach (var line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\DiagnosticReport.txt"))
            {
                diagnosticReport.Add(line);
            }

            FirstStar();
            SecondStar();

        }

        static void FirstStar()
        {
            int[] numOfOnes = new int[12];
            int[] numOfZeros = new int[12];
            string gamma = "";
            string epsilon = "";

            foreach (string binaryNumber in diagnosticReport)
            {
                for (int index = 0; index < binaryNumber.Length; index++)
                {
                    if (binaryNumber[index] == '1')
                    {
                        numOfOnes[index]++;
                    }
                }
            }

            for (int index = 0; index < numOfOnes.Length; index++)
            {
                numOfZeros[index] = diagnosticReport.Count - numOfOnes[index];

                if (numOfOnes[index] > numOfZeros[index])
                {
                    gamma += '1';
                    epsilon += '0';

                }
                else
                {
                    gamma += '0';
                    epsilon += '1';
                }
            }

            var gammaRate = Convert.ToInt32(gamma, 2);
            var epsilonRate = Convert.ToInt32(epsilon, 2);

            Console.WriteLine(gammaRate * epsilonRate);
            return;
        }

        static void SecondStar()
        {
            List<string> oxygenGeneratorRating = new List<string>();
            List<string> co2ScrubberRating = new List<string>();

            foreach (var number in diagnosticReport)
            {
                oxygenGeneratorRating.Add(number);
                co2ScrubberRating.Add(number);
            }

            oxygenGeneratorRating = FindRating(oxygenGeneratorRating, 0, true);
            co2ScrubberRating = FindRating(co2ScrubberRating, 0, false);

            var oxygen = Convert.ToInt32(oxygenGeneratorRating.ElementAt(0), 2);
            var co2 = Convert.ToInt32(co2ScrubberRating.ElementAt(0), 2);

            Console.WriteLine(oxygen * co2);
            return;
        }

        static List<string> FindRating(List<string> list, int index, bool oxygen)
        {

            if (list.Count > 1)
            {
                var numberOfOnes = 0;

                numberOfOnes = list.Count(number => number[index] == '1');

                if (numberOfOnes >= list.Count - numberOfOnes)
                {
                    list = list.Where(number =>
                    {
                        if (oxygen && number[index] != '1')
                        {
                            return false;
                        }
                        else if (!oxygen && number[index] == '1')
                        {
                            return false;
                        }
                        return true;

                    }).ToList();
                }
                else
                {
                    list = list.Where(number =>
                    {
                        if (oxygen && number[index] != '0')
                        {
                            return false;
                        }
                        else if (!oxygen && number[index] == '0')
                        {
                            return false;
                        }
                        return true;

                    }).ToList();
                }

                list = FindRating(list, ++index, oxygen);
            }

            return list;
        }
    }
}
