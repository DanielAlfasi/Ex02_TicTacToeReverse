using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public static class ComputerPlayer
    {
        private static eMark m_Mark = eMark.O;

        public static int[] FindBestMove(Board board)
        {
            int bestScore = -int.MaxValue;
            int[] move = new int[] { -1, -1 };
            for (int i = 0; i < board.SequenceSize; i++)
            {
                for (int j = 0; j < board.SequenceSize; j++)
                {
                    // Try this spot
                    if (board.IsCellEmpty(i, j))
                    {
                        board.UpdateBoard(i, j, m_Mark);
                        int score = Minimax(board, 0, false);
                        board.UndoUpdateBoard(i, j, eMark.Empty); // undo move
                        if (score > bestScore)
                        {
                            bestScore = score;
                            move[0] = i;
                            move[1] = j;
                        }
                    }
                }
            }
            return move;
        }

        private static int Minimax(Board board, int depth, bool isMaximizing)
        {
            // if the current board state is a terminal state, return its score
            int score = EvaluateBoard(board);
            if (score == 1)
                return score;
            if (score == -1)
                return score;
            if (!board.IsBoardFull())
                return 0;

            if (isMaximizing)
            {
                int bestScore = -int.MaxValue;
                for (int i = 0; i < board.SequenceSize; i++)
                {
                    for (int j = 0; j < board.SequenceSize; j++)
                    {
                        if (board.IsCellEmpty(i, j))
                        {
                            board.UpdateBoard(i, j, m_Mark);
                            int score1 = Minimax(board, depth + 1, false);
                            board.UndoUpdateBoard(i, j, eMark.Empty); // undo move
                            bestScore = Math.Max(score1, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.SequenceSize; i++)
                {
                    for (int j = 0; j < board.SequenceSize; j++)
                    {
                        if (board.IsCellEmpty(i, j))
                        {
                            board.UpdateBoard(i, j, eMark.X);
                            int score2 = Minimax(board, depth + 1, true);
                            board.UndoUpdateBoard(i, j, eMark.Empty); // undo move
                            bestScore = Math.Min(score2, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        // Evaluate the favorability of the current board state
        private static int EvaluateBoard(Board board)
        {
            for(int i = 0; i < board.SequenceSize;i++)
            {
                for (int j = 0; j < board.SequenceSize; j++)
                {
                    if (Game.IsVictory(board, i, j))
                    {
                        if(board.CellContent(i, j) == eMark.O)
                        {
                            return -1;
                        }
                        else if (board.CellContent(i, j) == eMark.X)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                
            }
            return 0;
        }
    }

}
