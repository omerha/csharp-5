using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex05
{
    public class Player
    {
        private int m_NumOfPoints = 0;
        private char m_Sign;
        private readonly bool m_IsComputer = false;
        private readonly string m_PlayerName;

        public Player(string i_PlayerName,char i_Sign)
        {
            if(i_PlayerName == "[Computer]")
            {
                m_IsComputer = true;
                m_PlayerName = "Computer";
            }
            m_Sign = i_Sign;
            m_PlayerName = i_PlayerName;
        }
        public string PlayerName
        {
            get { return m_PlayerName; }
        }
        public bool IsComputer
        {
            get { return m_IsComputer; }
        }
        public char Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public int Points
        {
            get { return m_NumOfPoints; }
            set { m_NumOfPoints = value; }
        }

    }
}
