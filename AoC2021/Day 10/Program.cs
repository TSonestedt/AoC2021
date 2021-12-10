using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
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
            var syntaxChunks = new List<string>();
            foreach (var line in File.ReadLines(@$"{Directory.GetCurrentDirectory()}\NavigationSubsystem.txt"))
            {
                syntaxChunks.Add(line);
            }

            return syntaxChunks;
        }
        static void FirstStar()
        {
            var input = ReadInput();

            var firstIlleagalChars = new List<char>();

            foreach (var line in input)
            {
                var expectedMatchingChar = new List<char>();
                foreach (var symbol in line)
                {
                    if (symbol == '(' || symbol == '[' || symbol == '{' || symbol == '<')
                    {
                        expectedMatchingChar.Add(symbol);
                    }
                    else if (symbol == expectedMatchingChar.Last() + 2)
                    {
                        expectedMatchingChar.RemoveAt(expectedMatchingChar.Count - 1);
                    }
                    else if (symbol == ')' && expectedMatchingChar.Last() == '(')
                    {
                        expectedMatchingChar.RemoveAt(expectedMatchingChar.Count - 1);
                    }
                    else
                    {
                        firstIlleagalChars.Add(symbol);
                        break;
                    }
                }
            }

            var syntaxErrorScore = 0;
            foreach (var symbol in firstIlleagalChars)
            {
                switch (symbol)
                {
                    case ')':
                        syntaxErrorScore += 3;
                        break;
                    case ']':
                        syntaxErrorScore += 57;
                        break;
                    case '}':
                        syntaxErrorScore += 1197;
                        break;
                    case '>':
                        syntaxErrorScore += 25137;
                        break;
                    default:
                        break;
                }

            }

            Console.WriteLine(syntaxErrorScore);

        }
        static void SecondStar()
        {
            var input = ReadInput();

            var scores = new List<long>();

            foreach (var line in input)
            {
                var expectedMatchingChar = new List<char>();
                foreach (var symbol in line)
                {
                    if (symbol == '(' || symbol == '[' || symbol == '{' || symbol == '<')
                    {
                        expectedMatchingChar.Add(symbol);
                    }
                    else if (symbol == expectedMatchingChar.Last() + 2)
                    {
                        expectedMatchingChar.RemoveAt(expectedMatchingChar.Count - 1);
                    }
                    else if (symbol == ')' && expectedMatchingChar.Last() == '(')
                    {
                        expectedMatchingChar.RemoveAt(expectedMatchingChar.Count - 1);
                    }
                    else
                    {
                        expectedMatchingChar = null;
                        break;
                    }

                }
                                
                if (expectedMatchingChar != null)
                {
                    long totalScore = 0;

                    expectedMatchingChar.Reverse();

                    foreach (var symbol in expectedMatchingChar)
                    {
                        totalScore = totalScore * 5;

                        switch (symbol)
                        {
                            case '(':
                                totalScore += 1;
                                break;
                            case '[':
                                totalScore += 2;
                                break;
                            case '{':
                                totalScore += 3;
                                break;
                            case '<':
                                totalScore += 4;
                                break;
                            default:
                                break;
                        }
                    }

                    scores.Add(totalScore);

                }


            }

            scores.Sort();
            Console.WriteLine(scores[scores.Count() / 2]);
        }

    }
}