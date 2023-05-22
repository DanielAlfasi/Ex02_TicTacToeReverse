using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class Board
    {
        private readonly eMark[,] r_Board;
        private int m_NumberOfEmptyCells;
        private int m_SequenceSize;

        public Board(int i_Size)
        {
            this.r_Board = new eMark[i_Size,i_Size];
            this.m_NumberOfEmptyCells = i_Size * i_Size;
            this.m_SequenceSize = i_Size;
            for (int i = 0 ; i < i_Size ; i++)
            {
                for (int j = 0 ; j < i_Size ; j++)
                {
                    this.r_Board[i,j] = eMark.Empty;
                }
            }
        }

        public int NumberOfEmptyCells
        {
            get { return m_NumberOfEmptyCells; }
        }

        public int SequenceSize
        {
            get { return this.m_SequenceSize; }
        }

        public eMark[,] BoardState
        {
            get { return this.r_Board; }
            
        }

        public eMark CellContent(int i_RowIndex, int i_ColumnIndex)
        {
            return BoardState[i_RowIndex,i_ColumnIndex];
        }

        public void UpdateBoard(int i_RowIndex, int i_ColumnIndex, eMark i_Mark)
        {
            this.r_Board[i_RowIndex,i_ColumnIndex] = i_Mark;
            this.m_NumberOfEmptyCells = m_NumberOfEmptyCells - 1;
        }

        public bool IsCellEmpty(int i_RowIndex, int i_ColumnIndex)
        {
            return BoardState[i_RowIndex,i_ColumnIndex] == eMark.Empty;
        }

        public bool IsBoardFull()
        {
            return NumberOfEmptyCells == 0;
        }

        public bool IsInBoardBounds(int i_RowIndex, int i_ColumnIndex)
        {
            return i_RowIndex >= 0 && i_ColumnIndex >= 0 && i_RowIndex < SequenceSize && i_ColumnIndex < SequenceSize;
        }

        public void UndoUpdateBoard(int i_RowIndex, int i_ColumnIndex, eMark i_Mark)
        {
            this.r_Board[i_RowIndex,i_ColumnIndex] = i_Mark;
            this.m_NumberOfEmptyCells++;
        }

        public void ResetBoard()
        {
            for (int i = 0 ; i < SequenceSize ; i++)
            {
                for (int j = 0 ; j < SequenceSize ; j++)
                {
                    this.r_Board[i,j] = eMark.Empty;
                }
            }
            m_NumberOfEmptyCells = SequenceSize * SequenceSize;
        }
    }
}
