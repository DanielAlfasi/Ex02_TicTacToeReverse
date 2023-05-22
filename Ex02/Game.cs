using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public static class Game
    {
        public const int k_MaxSizeForBoard = 9;
        public const int k_MinSizeForBoard = 3;
        public static bool IsVictory(Board i_Board, int i_RowIndex, int i_ColumnIndex)
        {
            eMark markToCheck = i_Board.CellContent(i_RowIndex, i_ColumnIndex);
            return IsRowMarked(i_Board, i_RowIndex, markToCheck) || IsColumnMarked(i_Board, i_ColumnIndex, markToCheck) || isMainDiagonalMarked(i_Board, markToCheck) || IsSecondaryDiagonalMarked(i_Board, markToCheck);
        }

        public static bool IsRowMarked(Board i_Board, int i_RowIndex, eMark i_Mark)
        {
            bool rowCompleted = true;
            for (int i = 0 ; i < i_Board.SequenceSize ; i++)
            {
                if (i_Board.CellContent(i_RowIndex, i) != i_Mark)
                {
                    rowCompleted = false;
                    break;
                }
            }
            return rowCompleted;
        }

        public static bool IsColumnMarked(Board i_Board, int i_ColumnIndex, eMark i_Mark)
        {
            bool columnCompleted = true;
            for (int i = 0 ; i < i_Board.SequenceSize ; i++)
            {
                if (i_Board.CellContent(i, i_ColumnIndex) != i_Mark)
                {
                    columnCompleted = false;
                    break;
                }
            }
            return columnCompleted;
        }

        public static bool isMainDiagonalMarked(Board i_Board, eMark i_Mark)
        {
            bool mainDiagonalCompleted = true;
            for (int i = 0 ; i < i_Board.SequenceSize ; i++)
            {
                if (i_Board.CellContent(i, i) != i_Mark)
                {
                    mainDiagonalCompleted = false;
                    break;
                }
            }
            return mainDiagonalCompleted;
        }

        public static bool IsSecondaryDiagonalMarked(Board i_Board, eMark i_Mark)
        {
            bool secondaryDiagonalCompleted = true;
            for (int i = i_Board.SequenceSize - 1 ; i >= 0 ; i--)
            {
                if (i_Board.CellContent(i_Board.SequenceSize - 1 - i, i) != i_Mark)
                {
                    secondaryDiagonalCompleted = false;
                    break;
                }
            }
            return secondaryDiagonalCompleted;
        }

        public static bool IsValidMove(int i_RowIndex, int i_ColumnIndex, Board i_Board)
        {
            return i_Board.IsInBoardBounds(i_RowIndex, i_ColumnIndex) && i_Board.IsCellEmpty(i_RowIndex, i_ColumnIndex);
        }
    }
}
