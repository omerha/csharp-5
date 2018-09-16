using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public class BoardLogic
    {
        private const int k_NumOfSignsSequenceToWin = 4;
        private Button[,] m_GameBoard = null;
        private readonly int r_BoardCols = 0;
        private readonly int r_BoardRows = 0;
        private List<Button> m_ClickAbleButtons;
        public BoardLogic(int i_NumOfCols, int i_NumOfRow, Button[,] i_GameBoard)
        {
            r_BoardCols = i_NumOfCols;
            r_BoardRows = i_NumOfRow;
            m_GameBoard = i_GameBoard;
        }
        public int NumOfRows
        {
            get { return r_BoardRows; }
        }
        public int NumOfCols
        {
            get { return r_BoardCols; }
        }
        public Button[,] GameBoard
        {
            get { return m_GameBoard; }
        }
        public List<Button> ClickAbleButtons
        {
            get { return m_ClickAbleButtons; }
            set { m_ClickAbleButtons = value; }
        }

        public void InitializationGameBoard()
        {
            for (int i = 0; i < r_BoardRows; i++)
            {
                for (int j = 0; j < r_BoardCols; j++)
                {
                    m_GameBoard[i, j].Text = string.Empty;
                }
            }
        }
        public void GameBoardUpdateAndCheckIfFull(int i_Move, char i_Sign)
        {
            bool colIsFull = false;
            for (int i = r_BoardRows; i > 0; i--)
            {
                if (m_GameBoard[i - 1, i_Move - 1].Text == string.Empty)
                {
                    m_GameBoard[i - 1, i_Move - 1].Text = i_Sign.ToString();
                    m_GameBoard[i - 1, i_Move - 1].Font = new Font(SystemFonts.DefaultFont.FontFamily, 9, FontStyle.Bold);
                    if (i == 1)
                    {
                        colIsFull = true;
                    }
                    break;
                }
            }
            if(colIsFull)
            {
                SetColFull(i_Move);
            }
        }
        public void GetMoveFromComputer(ref int o_MoveOfPlayer)
        {
            Random rander = new Random();
            do
            {
                o_MoveOfPlayer = rander.Next(1, r_BoardCols + 1);
            }while (m_ClickAbleButtons[o_MoveOfPlayer - 1].Enabled == false);
        
        }
        public void SetColFull(int i_ColToBlock)
        {
            m_ClickAbleButtons[i_ColToBlock - 1].Enabled = false;
        }
        public void ClearBoard()
        {
            foreach(Button clickAbleButton in m_ClickAbleButtons)
            {
                clickAbleButton.Enabled = true;
            }
            foreach(Button cellButton in m_GameBoard)
            {
                cellButton.Text = string.Empty;
            }
        }
        public bool IsThereWinner(char i_Sign)
        {
            bool thereIsWinner = false;
            if (CheckRowsForWinner(i_Sign))
            {
                thereIsWinner = true;
            }

            if (CheckColsForWinner(i_Sign) && !thereIsWinner)
            {
                thereIsWinner = true;
            }

            if (CheckDiagonalForWinner(i_Sign) && !thereIsWinner)
            {
                thereIsWinner = true;
            }

            return thereIsWinner;
        }
        public bool IsFullBoard()
        {
            bool isFull = true;
            foreach(Button clickAbleButton in m_ClickAbleButtons)
            {
                if(clickAbleButton.Enabled == true)
                {
                    isFull = false;
                }
            }

            return isFull;
        }
        private bool CheckRowsForWinner(char i_Sign)
        {
            int counterSingInRows = 0;
            bool thereIsWinner = false;
            for (int i = 0; i < r_BoardRows && !thereIsWinner; i++)
            {
                counterSingInRows = 0;
                for (int j = 0; j < r_BoardCols && !thereIsWinner; j++)
                {
                    if (m_GameBoard[i, j].Text == i_Sign.ToString())
                    {
                        counterSingInRows++;
                        if (counterSingInRows == k_NumOfSignsSequenceToWin)
                        {
                            thereIsWinner = true;
                        }
                    }
                    else
                    {
                        counterSingInRows = 0;
                    }
                }
            }

            return thereIsWinner;
        }
        private bool CheckColsForWinner(char i_Sign)
        {
            int counterSingInCols = 0;
            bool thereIsWinner = false;
            for (int j = 0; j < r_BoardCols && !thereIsWinner; j++)
            {
                counterSingInCols = 0;
                for (int i = 0; i < r_BoardRows && !thereIsWinner; i++)
                {
                    if (m_GameBoard[i, j].Text == i_Sign.ToString())
                    {
                        counterSingInCols++;
                        if (counterSingInCols == k_NumOfSignsSequenceToWin)
                        {
                            thereIsWinner = true;
                        }
                    }
                    else
                    {
                        counterSingInCols = 0;
                    }
                }
            }

            return thereIsWinner;
        }

        private bool CheckDiagonalForWinner(char i_Sign)
        {
            int rowUpIndex = 0, rowDownIndex = 0, colIndex = 0, counterUpDiagonal = 0, counterDownDiagonal = 0;
            bool thereIsWinner = false;
            for (int i = 0; i < r_BoardRows - 3 && !thereIsWinner; i++)
            {
                for (int j = 0; j < r_BoardCols - 3 && !thereIsWinner; j++)
                {
                    rowUpIndex = r_BoardRows - 1 - i;
                    rowDownIndex = i;
                    colIndex = j;
                    counterDownDiagonal = 0;
                    counterUpDiagonal = 0;
                    while (colIndex < r_BoardCols && rowUpIndex >= 0 && rowDownIndex < r_BoardRows && !thereIsWinner)
                    {
                        if (m_GameBoard[rowUpIndex, colIndex].Text == i_Sign.ToString() && m_GameBoard[rowDownIndex, colIndex].Text == i_Sign.ToString())
                        {
                            counterDownDiagonal++;
                            counterUpDiagonal++;
                        }
                        else
                        {
                            if (m_GameBoard[rowUpIndex, colIndex].Text == i_Sign.ToString())
                            {
                                counterUpDiagonal++;
                                counterDownDiagonal = 0;
                            }
                            else if (m_GameBoard[rowDownIndex, colIndex].Text == i_Sign.ToString())
                            {
                                counterDownDiagonal++;
                                counterUpDiagonal = 0;
                            }
                            else
                            {
                                counterUpDiagonal = 0;
                                counterDownDiagonal = 0;
                            }
                        }

                        if (counterUpDiagonal == k_NumOfSignsSequenceToWin || counterDownDiagonal == k_NumOfSignsSequenceToWin)
                        {
                            thereIsWinner = true;
                        }

                        colIndex++;
                        rowDownIndex++;
                        rowUpIndex--;
                    }
                }
            }

            return thereIsWinner;
        }
    }
}

