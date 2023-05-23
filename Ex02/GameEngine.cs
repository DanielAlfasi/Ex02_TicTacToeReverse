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
        private int m_PlayerTurnIndex;
        private int m_LastRowModified;
        private int m_LastColumnModified;
        private bool m_IsGameEndWithTie;
        private const int k_FirstPlayerIndex = 0;
        private const int k_SecondPlayerIndex = 1;
        private const eMark k_FirstPlayerMark = eMark.X;
        private const eMark k_SecondPlayerMark = eMark.O;
        private const ePlayerType k_FirstPlayerType = ePlayerType.Person;

        public GameEngine(int i_BoardSize, ePlayerType i_SecondPlayerType)
        {
            this.m_Players = new Player[Game.NumberOfPlayers];
            this.m_Board = new Board(i_BoardSize);
            this.m_Players[k_FirstPlayerIndex] = new Player(k_FirstPlayerMark, k_FirstPlayerType);
            this.m_Players[k_SecondPlayerIndex] = new Player(k_SecondPlayerMark, i_SecondPlayerType);
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
            if (Board.CellContent(this.m_LastRowModified, this.m_LastColumnModified) != eMark.Empty && Game.IsVictory(this.m_Board, this.m_LastRowModified, this.m_LastColumnModified))
            {
                if (Board.CellContent(this.m_LastRowModified, this.m_LastColumnModified) == eMark.X)
                {
                    this.m_Players[k_SecondPlayerIndex].Score++;
                    this.m_RoundWinner = this.m_Players[k_SecondPlayerIndex];
                }
                else
                {
                    this.m_Players[k_FirstPlayerIndex].Score++;
                    this.m_RoundWinner = this.m_Players[k_FirstPlayerIndex];
                }
                IsGameOverWithVictory = true;
            }
            this.m_IsGameEndWithTie = Board.IsBoardFull() && !IsGameOverWithVictory;
            return this.m_IsGameEndWithTie || IsGameOverWithVictory;
        }

        public Player GetWinner()
        {
            return this.m_RoundWinner;
        }

        public bool PerformMove(int i_RowIndex, int i_ColumnIndex)
        {
            bool isMoveWasPerformed = false;
            if(Game.IsValidMove(i_RowIndex, i_ColumnIndex, this.m_Board))
            {
                PerformPlayerMove(i_RowIndex, i_ColumnIndex);
                if (m_Players[k_SecondPlayerIndex].PlayerType == ePlayerType.Computer && (!Board.IsBoardFull() && !Game.IsVictory(m_Board, this.m_LastRowModified, this.m_LastColumnModified)))
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
                coords = ComputerPlayer.FindBestMove(this.m_Board);
                this.m_Board.UpdateBoard(coords[0], coords[1], k_SecondPlayerMark);
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
