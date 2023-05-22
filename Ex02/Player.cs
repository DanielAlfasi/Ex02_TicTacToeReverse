using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public struct Player
    {
        private ePlayerType m_PlayerType;
        private eMark m_Mark;
        private int m_Score;

        public Player(eMark i_Mark, ePlayerType i_PlayerType)
        {
            m_Mark = i_Mark;
            m_PlayerType = i_PlayerType; 
            m_Score = 0;
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
        }

        public eMark Mark
        {
            get { return this.m_Mark; }
        }

        public int Score
        { 
            get { return m_Score; } 
            set { m_Score = value; }
        }
    }
}
