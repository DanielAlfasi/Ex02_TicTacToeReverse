using System;

namespace Ex02
{
    public static class ComputerPlayer
    {
        private static eMark s_ComputerPlayerMark = eMark.O;
        private static eMark s_HumanPlayerMark = eMark.X;
        private static eMark s_EmptyMark = eMark.Empty;
        private static Random s_RandomGenerator = new Random();
        private const int k_ReturnValueForGoodScore = 1;
        private const int k_ReturnValueForBadScore = -1;
        private const int ScoreForTie = 0;
        private const int k_RecursionMaxDepth = 9;
        private const bool k_PlayerToMax = true;
        private const bool k_PlayerToMin = false;

        public static (int, int) GetNextMove(Board i_Board)
        {
            int bestScoreForMove = int.MinValue;
            int scoreForCurrentMove;
            (int, int) bestMoveCoords = (-1, -1);

            if (i_Board.NumberOfEmptyCells <= k_RecursionMaxDepth)
            {
                for (int i = 0; i < i_Board.SequenceSize; i++)
                {
                    for (int j = 0; j < i_Board.SequenceSize; j++)
                    {
                        if (i_Board.IsCellEmpty(i, j))
                        {
                            i_Board.UpdateBoard(i, j, s_ComputerPlayerMark);
                            scoreForCurrentMove = miniMax(i_Board, 0, k_PlayerToMin, i, j);
                            i_Board.UpdateBoard(i, j, s_EmptyMark);
                            if (scoreForCurrentMove > bestScoreForMove)
                            {
                                bestMoveCoords = (i, j);
                                bestScoreForMove = scoreForCurrentMove;
                            }
                        }
                    }
                }
            }
            if (scoreHaveNotAssignedByTree(bestScoreForMove))
            {
                bestMoveCoords = generateEmptyRandomCell(i_Board);
            }

            return bestMoveCoords;
        }

        private static int miniMax(Board i_Board, int i_Depth, bool i_MinOrMax, int i_LastRowModified, int i_LastColumnModified)
        {
            int bestScore, currentMoveScore, scoreToReturn;


            if (i_Depth > k_RecursionMaxDepth)
            {
                scoreToReturn = valueForMaxDepth(i_MinOrMax);
            }
            else if (checkIfSpecificPlayerWon(i_Board, i_LastRowModified, i_LastColumnModified, s_ComputerPlayerMark))
            {
                scoreToReturn = k_ReturnValueForBadScore;
            }
            else if (checkIfSpecificPlayerWon(i_Board, i_LastRowModified, i_LastColumnModified, s_HumanPlayerMark))
            {
                scoreToReturn = k_ReturnValueForGoodScore;
            }
            else if (i_Board.IsBoardFull())
            {
                scoreToReturn = ScoreForTie;
            }
            else if (i_MinOrMax)
            {
                bestScore = int.MinValue;
                for (int i = 0; i < i_Board.SequenceSize; i++)
                {
                    for (int j = 0; j < i_Board.SequenceSize; j++)
                    {
                        if (i_Board.IsCellEmpty(i, j))
                        {
                            i_Board.UpdateBoard(i, j, s_ComputerPlayerMark);
                            currentMoveScore = miniMax(i_Board, i_Depth + 1, k_PlayerToMin, i, j);
                            i_Board.UpdateBoard(i, j, s_EmptyMark);

                            if(bestScore < currentMoveScore)
                            {
                                bestScore = currentMoveScore;
                            }
                        }
                    }
                }
                scoreToReturn = bestScore;
            }
            else
            {
                bestScore = int.MaxValue;
                for (int i = 0; i < i_Board.SequenceSize; i++)
                {
                    for (int j = 0; j < i_Board.SequenceSize; j++)
                    {
                        if (i_Board.IsCellEmpty(i, j))
                        {
                            i_Board.UpdateBoard(i, j, s_HumanPlayerMark);
                            currentMoveScore = miniMax(i_Board, i_Depth + 1, k_PlayerToMax, i, j);
                            i_Board.UpdateBoard(i, j, s_EmptyMark);

                            if (bestScore > currentMoveScore)
                            {
                                bestScore = currentMoveScore;
                            }
                        }
                    }
                }
                scoreToReturn = bestScore;
            }

            return scoreToReturn;
        }

        private static int valueForMaxDepth(bool i_IsPlayerMinOrMax)
        {
            int valueForMinOrMax;

            if (i_IsPlayerMinOrMax)
            {
                valueForMinOrMax = int.MaxValue;
            }
            else
            {
                valueForMinOrMax = int.MinValue;
            }

            return valueForMinOrMax;
        }

        private static bool checkIfSpecificPlayerWon(Board i_Board, int i_RowIndex, int i_ColumnIndex, eMark i_PlayerMark)
        {
            return Game.IsVictory(i_Board, i_RowIndex, i_ColumnIndex) && i_Board.CellContent(i_RowIndex
                , i_ColumnIndex) == i_PlayerMark;
        }

        private static (int, int) generateEmptyRandomCell(Board i_Board)
        {
            int NumberOfFreeCells = s_RandomGenerator.Next(i_Board.EmptyCellsList.Count);

            return i_Board.EmptyCellsList[NumberOfFreeCells];
        }

        private static bool scoreHaveNotAssignedByTree(int i_ScoreToCheck)
        {
            return i_ScoreToCheck == int.MinValue || i_ScoreToCheck == int.MaxValue;
        }

    }  
}