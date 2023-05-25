namespace Ex02
{
    public static class Game
    {
        private const int k_MaxSizeForBoard = 9;
        private const int k_MinSizeForBoard = 3;
        private const int k_NumberOfPlayers = 2;

        public static int NumberOfPlayers
        {
            get { return k_NumberOfPlayers; }
        }

        public static int MaxSizeForBoard
        {
            get { return k_MaxSizeForBoard; }
        }

        public static int MinSizeForBoard
        {
            get { return k_MinSizeForBoard; }
        }

        internal static bool IsVictory(Board i_Board, int i_RowIndex, int i_ColumnIndex)
        {
            eMark markToCheck = i_Board.CellContent(i_RowIndex, i_ColumnIndex);
            return isRowMarked(i_Board, i_RowIndex, markToCheck) || isColumnMarked(i_Board, i_ColumnIndex, markToCheck) || isMainDiagonalMarked(i_Board, markToCheck) || isSecondaryDiagonalMarked(i_Board, markToCheck);
        }

        internal static bool IsValidMove(int i_RowIndex, int i_ColumnIndex, Board i_Board)
        {
            return i_Board.IsInBoardBounds(i_RowIndex, i_ColumnIndex) && i_Board.IsCellEmpty(i_RowIndex, i_ColumnIndex);
        }

        private static bool isRowMarked(Board i_Board, int i_RowIndex, eMark i_Mark)
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

        private static bool isColumnMarked(Board i_Board, int i_ColumnIndex, eMark i_Mark)
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

        private static bool isMainDiagonalMarked(Board i_Board, eMark i_Mark)
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

        private static bool isSecondaryDiagonalMarked(Board i_Board, eMark i_Mark)
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

    }
}
