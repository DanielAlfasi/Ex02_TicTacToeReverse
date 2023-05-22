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
        private int m_PlayerTurnIndex = 0;
        private int m_LastRowModified;
        private int m_LastColumnModified;
        private Player m_RoundWinner;
        private bool m_IsGameEndWithTie;

        public GameEngine(int i_BoardSize, ePlayerType i_SecondPlayerType)
        {
            this.m_Players = new Player[2];
            m_Board = new Board(i_BoardSize);
            m_Players[0] = new Player(eMark.X, ePlayerType.Person);
            m_Players[1] = new Player(eMark.O, i_SecondPlayerType);
            this.m_LastRowModified = 0;
            this.m_LastColumnModified = 0;
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
            if (Board.CellContent(m_LastRowModified, m_LastColumnModified) != eMark.Empty && Game.IsVictory(Board, m_LastRowModified, m_LastColumnModified))
            {
                if (Board.CellContent(m_LastRowModified, m_LastColumnModified) == eMark.X)
                {
                    m_Players[1].Score++;
                    m_RoundWinner = m_Players[1];
                }
                else
                {
                    m_Players[0].Score++;
                    m_RoundWinner = m_Players[0];
                }
                return true;
            }


            this.m_IsGameEndWithTie = Board.IsBoardFull();
            return m_IsGameEndWithTie;
        }

        public Player GetWinner()
        {
            return m_RoundWinner;
        }

        public bool PerformMove(int i_RowIndex, int i_ColumnIndex)
        {
            if(Game.IsValidMove(i_RowIndex, i_ColumnIndex, Board))
            {
                PerformPlayerMove(i_RowIndex, i_ColumnIndex);
                if (m_Players[1].PlayerType == ePlayerType.Computer && (!Board.IsBoardFull() && !Game.IsVictory(m_Board, m_LastRowModified, m_LastColumnModified)))
                {
                    PerformComputerMove();
                }
                
                return true;
            }
            return false;
        }

        public void PerformPlayerMove(int i_RowIndex, int i_ColumnIndex)
        {
            m_Board.UpdateBoard(i_RowIndex, i_ColumnIndex, m_Players[(m_PlayerTurnIndex % 2)].Mark);
            m_LastRowModified = i_RowIndex;
            m_LastColumnModified = i_ColumnIndex;
            this.m_PlayerTurnIndex++;
        }

        public void PerformComputerMove()
        {

                int[] coords = new int[2];
                coords = ComputerPlayer.FindBestMove(Board);
                m_Board.UpdateBoard(coords[0], coords[1], eMark.O);
                m_LastRowModified = coords[0];
                m_LastColumnModified = coords[1];
                this.m_PlayerTurnIndex++;
        }

        public void ResetGame()
        {
            m_Board.ResetBoard();
            m_PlayerTurnIndex = 0;
            this.m_LastRowModified = 0;
            this.m_LastColumnModified = 0;
            this.m_IsGameEndWithTie = false;
        }

    }
}
