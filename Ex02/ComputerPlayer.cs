using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public static class ComputerPlayer
    {
        private static eMark s_Mark = eMark.O;

        internal static int[] FindBestMove(Board board)
        {
            int bestScore = -int.MaxValue;
            int[] coords = new int[]{-1,-1};
            for (int i = 0 ; i < board.SequenceSize ; i++)
            {
                for (int j = 0 ; j < board.SequenceSize ; j++)
                {
                    if (board.IsCellEmpty(i, j))
                    {
                        board.UpdateBoard(i, j, s_Mark);
                        int score = minimax(board, 0, false);
                        board.UpdateBoard(i, j, eMark.Empty);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            coords[0] = i;
                            coords[1] = j;
                        }
                    }
                }
            }
            return coords;
        }

        private static int minimax(Board i_Board, int i_Depth, bool i_IsMaximizing)
        {
            int valueToReturn;
            int score = evaluateBoard(i_Board);
            if (score == 1 || score == -1)
            {
                valueToReturn = score;
            }
            else if (!i_Board.IsBoardFull())
            {
                valueToReturn = 0;
            } else if(i_IsMaximizing)
            {
                int bestScore = -int.MaxValue;
                for (int i = 0 ; i < i_Board.SequenceSize ; i++)
                {
                    for (int j = 0 ; j < i_Board.SequenceSize ; j++)
                    {
                        if (i_Board.IsCellEmpty(i, j))
                        {
                            i_Board.UpdateBoard(i, j, s_Mark);
                            int score1 = minimax(i_Board, i_Depth + 1, false);
                            i_Board.UpdateBoard(i, j, eMark.Empty);
                            bestScore = Math.Max(score1, bestScore);
                        }
                    }
                }
                valueToReturn = bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0 ; i < i_Board.SequenceSize ; i++)
                {
                    for (int j = 0 ; j < i_Board.SequenceSize ; j++)
                    {
                        if (i_Board.IsCellEmpty(i, j))
                        {
                            i_Board.UpdateBoard(i, j, eMark.X);
                            int score2 = minimax(i_Board, i_Depth + 1, true);
                            i_Board.UpdateBoard(i, j, eMark.Empty);
                            bestScore = Math.Min(score2, bestScore);
                        }
                    }
                }
                valueToReturn = bestScore;
            }
            return valueToReturn;
        }

        private static int evaluateBoard(Board i_Board)
        {
            int valueToReturn = 0;
            bool breakLoops = false;
            for(int i = 0 ; i < i_Board.SequenceSize ; i++)
            {
                for (int j = 0 ; j < i_Board.SequenceSize ; j++)
                {
                    if (Game.IsVictory(i_Board, i, j))
                    {
                        if(i_Board.CellContent(i, j) == eMark.O)
                        {
                            valueToReturn = -1;
                            break;
                        }
                        else if(i_Board.CellContent(i, j) == eMark.X)
                        {
                            valueToReturn = 1;
                            break;
                        }
                        else
                        {
                            valueToReturn = 0;
                            break;
                        }
                    }
                }
                if (breakLoops)
                {
                    break;
                }

            }
            return valueToReturn;
        }
    }

}
