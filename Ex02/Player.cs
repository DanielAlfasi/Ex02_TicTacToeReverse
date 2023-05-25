namespace Ex02
{
    public struct Player
    {
        private ePlayerType m_PlayerType;
        private eMark m_Mark;
        private int m_Score;

        public Player(eMark i_Mark, ePlayerType i_PlayerType)
        {
            this.m_Mark = i_Mark;
            this.m_PlayerType = i_PlayerType; 
            this.m_Score = 0;
        }

        public ePlayerType PlayerType
        {
            get { return this.m_PlayerType; }
        }

        public eMark Mark
        {
            get { return this.m_Mark; }
        }

        public int Score
        { 
            get { return this.m_Score; } 
            set { this.m_Score = value; }
        }
    }
}
