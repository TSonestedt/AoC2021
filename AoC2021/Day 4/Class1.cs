using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    class Class1
    {


        static void Main(string[] args)
        {

            FirstStar();
            SecondStar();

        }

        static (List<int>, List<int[,]>) ReadInput()
        {
            List<int> numbers = new List<int>();
            List<int[,]> boards = new List<int[,]>();
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


                for (int row = 0; row < rows.Length; row++)
                {
                    string[] numbersInRow = rows[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int column = 0; column < numbersInRow.Length; column++)
                    {
                        board[row, column] = Convert.ToInt32(numbersInRow[column]);

                    }

                }

                boards.Add(board);

            }
            return new(numbers, boards);
        }
        static void FirstStar()
        {
            (var numbers, var boards) = ReadInput();

        }


        static void SecondStar()
        {

        }

    }
}
