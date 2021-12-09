using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
    class Program
    {

        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static List<string> ReadInput()
        {
            List<string> signalPatterns = new List<string>();

            foreach (var line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\SignalPatterns.txt"))
            {
                signalPatterns.Add(line);
            }

            return signalPatterns;
        }
        static void FirstStar()
        {
            List<string[]> fourDigitOutputs = ReadInput().Select(x => x.Split(" | ")[1].Split(" ")).ToList();

            var numberOf1s4s7sAnd8s = fourDigitOutputs.SelectMany(x => x.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7)).Count();

            Console.WriteLine(numberOf1s4s7sAnd8s);

            return;

        }

        static void SecondStar()
        {
            var signalPatterns = ReadInput().Select(x =>
            {
                var leftSide = x.Split(" | ")[0].Split(" ").Select(number => number.ToHashSet());
                var rightSide = x.Split(" | ")[1].Split(" ").Select(number => number.ToHashSet()).ToArray();

                return (leftSide, rightSide);

            }).ToList();

            var transalatedNumbers = new List<(HashSet<char>[] leftside, HashSet<char>[] righSide)>();

            foreach (var signalPattern in signalPatterns)
            {
                transalatedNumbers.Add(TranslateStringsToInt(signalPattern));
            }

            int outputValues = 0;

            foreach(var line in transalatedNumbers)
            {
                outputValues += Get4DigitOutput(line);
            }

            Console.WriteLine(outputValues);
            return;

            //6 signaler
            //9 innehåller alla bokstäver som fyra har + 2 till
            //0 har alla bokastäver som 7 + 3 till men skiljer sig från 4
            //6 skiljer sig från både 7 och fyra

            //5 signaler
            //3 innehåller alla som finns i 7 + 2 till
            //2
            //5s alla bokstäver förekommer i 6 och 9
        }

        static (HashSet<char>[] leftSide, HashSet<char>[] rightSide) TranslateStringsToInt((IEnumerable<HashSet<char>> leftSide, HashSet<char>[] rightSide) signalPattern)
        {
            HashSet<char>[] translatedNumbers = new HashSet<char>[10];

            translatedNumbers[1] = signalPattern.leftSide.Where(x => x.Count() == 2).First();

            translatedNumbers[4] = signalPattern.leftSide.Where(x => x.Count() == 4).First();

            translatedNumbers[7] = signalPattern.leftSide.Where(x => x.Count() == 3).First();

            translatedNumbers[8] = signalPattern.leftSide.Where(x => x.Count() == 7).First();

            translatedNumbers[9] = signalPattern.leftSide.Where(x =>
                {
                    if (x.Count() == 6)
                    {
                        HashSet<char> copy = new HashSet<char>(x);

                        copy.IntersectWith(translatedNumbers[4]);

                        if (copy.SetEquals(translatedNumbers[4]))
                        {
                            return true;
                        }

                    }
                    return false;
                }).First();

            translatedNumbers[0] = signalPattern.leftSide.Where(x =>
                {
                    if (x.Count() == 6)
                    {
                        HashSet<char> copy = new HashSet<char>(x);

                        copy.IntersectWith(translatedNumbers[7]);

                        if (copy.SetEquals(translatedNumbers[7]))
                        {
                            copy = new HashSet<char>(x);

                            copy.IntersectWith(translatedNumbers[4]);

                            if (!copy.SetEquals(translatedNumbers[4]))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }).First();

            translatedNumbers[6] = signalPattern.leftSide.Where(x =>
                {
                    if (x.Count() == 6)
                    {
                        HashSet<char> copy = new HashSet<char>(x);

                        copy.IntersectWith(translatedNumbers[7]);

                        if (!copy.SetEquals(translatedNumbers[7]))
                        {
                            return true;
                        }

                    }
                    return false;
                }).First();

            translatedNumbers[3] = signalPattern.leftSide.Where(x =>
            {
                if (x.Count() == 5)
                {
                    HashSet<char> copy = new HashSet<char>(x);

                    copy.IntersectWith(translatedNumbers[7]);

                    if (copy.SetEquals(translatedNumbers[7]))
                    {
                        return true;
                    }

                }
                return false;
            }).First();

            translatedNumbers[5] = signalPattern.leftSide.Where(x =>
            {
                if (x.Count() == 5)
                {
                    HashSet<char> copy = new HashSet<char>(x);

                    copy.IntersectWith(translatedNumbers[6]);

                    if (copy.SetEquals(x))
                    {
                        return true;
                    }

                }
                return false;
            }).First();

            translatedNumbers[2] = signalPattern.leftSide.Where(x =>
                {
                    if (x.Count() == 5)
                    {
                        HashSet<char> copy = new HashSet<char>(x);

                        copy.IntersectWith(translatedNumbers[7]);

                        if (!copy.SetEquals(translatedNumbers[7]))
                        {
                            copy = new HashSet<char>(x);
                            copy.IntersectWith(translatedNumbers[6]);

                            if (!copy.SetEquals(x))
                            {
                                return true;
                            }

                        }

                    }
                    return false;
                }).First();



            return (translatedNumbers, signalPattern.rightSide);
        }

        static int Get4DigitOutput((HashSet<char>[] leftSide, HashSet<char>[] rightSide) line){

            var fourDigitOutput = "";

            for (int i = 0; i < line.rightSide.Count(); i++)
            {
                for(int number = 0; number < line.leftSide.Count(); number++)
                {
                    if (line.leftSide[number].SetEquals(line.rightSide[i]))
                    {
                        fourDigitOutput += $"{number}";
                    }
                }
            }

            return Convert.ToInt32(fourDigitOutput);

        }
    }
}