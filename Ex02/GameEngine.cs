using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class GameEngine
    {
        private Board m_Board;
        private Player[] m_Players;
        private Player m_RoundWinner;
        private int m_PlayerTurnIndex = 0;
        private int m_LastRowModified;
        private int m_LastColumnModified;
        private bool m_IsGameEndWithTie;

        public GameEngine(int i_BoardSize, ePlayerType i_SecondPlayerType)
        {
            this.m_Players = new Player[2];
            this.m_Board = new Board(i_BoardSize);
            this.m_Players[0] = new Player(eMark.X, ePlayerType.Person);
            this.m_Players[1] = new Player(eMark.O, i_SecondPlayerType);
            this.m_LastRowModified = 0;
            this.m_LastColumnModified = 0;
            this.m_PlayerTurnIndex = 0;
            this.m_IsGameEndWithTie = false;
        }

        public bool IsGameEndWithTie
        {
            get { return m_IsGameEndWithTie; }
        }

        public Board Board
        { 
            get { return m_Board; }
        }

        public Player[] Players
        {
            get { return this.m_Players; }
        }

        public bool IsGameOver()
        {
            bool IsGameOverWithVictory = false;
            if (Board.CellContent(this.m_LastRowModified, this.m_LastColumnModified) != eMark.Empty && Game.IsVictory(Board, this.m_LastRowModified, this.m_LastColumnModified))
            {
                if (Board.CellContent(this.m_LastRowModified, this.m_LastColumnModified) == eMark.X)
                {
                    this.m_Players[1].Score++;
                    this.m_RoundWinner = this.m_Players[1];
                }
                else
                {
                    this.m_Players[0].Score++;
                    this.m_RoundWinner = this.m_Players[0];
                }
                IsGameOverWithVictory = true;
            }
            this.m_IsGameEndWithTie = Board.IsBoardFull();
            return this.m_IsGameEndWithTie || IsGameOverWithVictory;
        }

        public Player GetWinner()
        {
            return this.m_RoundWinner;
        }

        public bool PerformMove(int i_RowIndex, int i_ColumnIndex)
        {
            bool isMoveWasPerformed = false;
            if(Game.IsValidMove(i_RowIndex, i_ColumnIndex, Board))
            {
                PerformPlayerMove(i_RowIndex, i_ColumnIndex);
                if (m_Players[1].PlayerType == ePlayerType.Computer && (!Board.IsBoardFull() && !Game.IsVictory(m_Board, this.m_LastRowModified, this.m_LastColumnModified)))
                {
                    PerformComputerMove();
                }

                isMoveWasPerformed = true;
            }
            return isMoveWasPerformed;
        }

        public void PerformPlayerMove(int i_RowIndex, int i_ColumnIndex)
        {
            this.m_Board.UpdateBoard(i_RowIndex, i_ColumnIndex, this.m_Players[(m_PlayerTurnIndex % 2)].Mark);
            this.m_LastRowModified = i_RowIndex;
            this.m_LastColumnModified = i_ColumnIndex;
            this.m_PlayerTurnIndex++;
        }

        public void PerformComputerMove()
        {
                int[] coords = new int[2];
                coords = ComputerPlayer.FindBestMove(Board);
                this.m_Board.UpdateBoard(coords[0], coords[1], eMark.O);
                this.m_LastRowModified = coords[0];
                this.m_LastColumnModified = coords[1];
                this.m_PlayerTurnIndex++;
        }

        public void ResetGame()
        {
            this.m_Board.ResetBoard();
            this.m_PlayerTurnIndex = 0;
            this.m_LastRowModified = 0;
            this.m_LastColumnModified = 0;
            this.m_IsGameEndWithTie = false;
        }

    }
}
