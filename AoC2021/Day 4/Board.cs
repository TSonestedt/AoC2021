using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
    internal class Board
    {
        public (int number, bool marked)[][] Matrix { get; private set; }

        public bool IsBingo { get; private set; }

        public int? WinNumber { get; private set; }

        Board((int, bool)[][] matrix)
        {
            Matrix = matrix;
            IsBingo = false;
            WinNumber = null;

        }

        //private CheckIfBingo()
        //{
        //    var hasCompleteRow = true;
        //    var hasCompleteColumn = true;

        //    foreach( var row in Matrix)
        //    {
        //        foreach(var number in row)
        //        {
        //            if (!number.marked)
        //            {
        //                hasCompleteRow = false;
        //            }
        //        }
        //    }

        //    for(int column = 0; column < Matrix.GetLength(2); column++)
        //    {

        //    }
        //}
    }
}
