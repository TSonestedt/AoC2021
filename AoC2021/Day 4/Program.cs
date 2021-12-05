using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Program
    {
        static List<int> numbers = new List<int>();
        static List<int[,]> boards = new List<int[,]>();
        static List<int[,]> boards_copy = new List<int[,]>();

        static void Main(string[] args)
        {
            var input = File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\BingoData.txt");
            string[] inputs = input.Split("\r\n\r\n");

            string[] numbersFromFile = inputs[0].Split(",");

            foreach (string number in numbersFromFile)
            {
                numbers.Add(Convert.ToInt32(number));
            }

            for (int boardIndex = 1; boardIndex < inputs.Length; boardIndex++)
            {
                string[] rows = inputs[boardIndex].Split("\r\n");
                int[,] board = new int[5, 5];
                int[,] board_copy = new int[5, 5];


                for (int row = 0; row < rows.Length; row++)
                {
                    string[] numbersInRow = rows[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int column = 0; column < numbersInRow.Length; column++)
                    {
                        board[row, column] = Convert.ToInt32(numbersInRow[column]);
                        board_copy[row, column] = Convert.ToInt32(numbersInRow[column]);
                    }

                }

                boards.Add(board);
                boards_copy.Add(board_copy);

            }

            FirstStar();
            SecondStar();

        }

        static void FirstStar()
        {
            var score = 0;

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {

                    for (int row = 0; row < board.GetLength(0); row++)
                    {
                        for (int column = 0; column < board.GetLength(1); column++)
                        {
                            if (board[row, column] == number)
                            {
                                board[row, column] = -1;

                                if (CheckIfBingo(row, column, board))
                                {
                                    score = SumOfUnmarkedNumbers(board) * number;
                                    Console.WriteLine(score);
                                    return;
                                }

                            }
                        }
                    }
                }
            }

        }

        static bool CheckIfBingo(int row, int column, int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, column] > -1)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[row, j] > -1)
                        {
                            return false;
                        }
                    }

                }

            }
            return true;
        }

        static int SumOfUnmarkedNumbers(int[,] board)
        {
            int sum = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] > -1)
                    {
                        sum += board[i, j];
                    }
                }
            }
            return sum;
        }
        static void SecondStar()
        {
            var score = 0;
            var currentNumber = 0;
            var counter = 0;
            foreach (int number in numbers)
            {
                currentNumber = number;

                for (int row = 0; row < 5; row++)
                {
                    for (int column = 0; column < 5; column++)
                    {
                        boards_copy = boards_copy.Where(board =>
                        {
                            if (board[row, column] == number)
                            {
                                board[row, column] = -1;
                                if (CheckIfBingo(row, column, board))
                                {
                                    score = SumOfUnmarkedNumbers(board) * currentNumber;
                                    counter++;
                                    Console.WriteLine(@$"{counter}, {score}");
                                    return false;
                                }

                            }
                            return true;
                        }).ToList();
                    }
                }
            }
        }

    }
}