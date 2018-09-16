using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public partial class FormBoard : Form
    {
        private const char k_XSign = 'X';
        private const char k_OSign = 'O';
        private const int k_FirstPlayerTurn = 0;
        private const int k_SecondPlayerTurn = 1;
        private const int k_GapBetweenCells = 5;
        private const int k_ButtonSize = 40;
        private const int k_XStartLocation = 20;
        private const int k_YStartLocation = 20;
        private static int s_NumOfMoves = 0;
        private int m_Winner = 0;
        private int m_CurrentYSize = 0;
        private int m_CurrentXSize = 0;
        private Button[,] m_CellButtons;
        private List<Button> m_ClickAbleButtons;
        private BoardLogic m_BoardLogic;
        private Player[] m_Players = new Player[2];
        private Label[] m_PlayerNamesLabels = new Label[2];

        public FormBoard(int i_NumOfCols, int i_NumOfRows, string i_Player1Name, string i_Player2Name)
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                if (i == 0)
                {
                    m_Players[i] = new Player(i_Player1Name, k_XSign);
                }
                else
                {
                    m_Players[i] = new Player(i_Player2Name, k_OSign);
                }
            }

            m_CurrentXSize = k_XStartLocation + ((i_NumOfCols + 1) * (k_ButtonSize + k_GapBetweenCells));
            m_CurrentYSize = k_YStartLocation + ((i_NumOfRows + 2) * (k_ButtonSize + k_GapBetweenCells));
            m_CellButtons = new Button[i_NumOfRows, i_NumOfCols];
            m_BoardLogic = new BoardLogic(i_NumOfCols, i_NumOfRows, m_CellButtons);
            InitGameBoard();
            m_BoardLogic.ClickAbleButtons = m_ClickAbleButtons;
            AddPlayersLabels();
            InitializeComponent();
        }

        private void InitGameBoard()
        {
            int xPosition = k_XStartLocation;
            int yPosition = k_YStartLocation + k_GapBetweenCells + k_ButtonSize;
            initClickAbleButtons();
            for (int currRowNum = 0; currRowNum < m_BoardLogic.NumOfRows; currRowNum++)
            {
                for (int currColNum = 0; currColNum < m_BoardLogic.NumOfCols; currColNum++)
                {
                    Button cellButton = new Button();
                    cellButton.Name = string.Format("{0},{1}", currRowNum, currColNum);
                    cellButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    cellButton.Location = new Point(xPosition, yPosition);
                    cellButton.TextChanged += CellButtonTextChanged;
                    cellButton.BackColorChanged += CellButtonTextChanged;
                    m_CellButtons[currRowNum, currColNum] = cellButton;
                    xPosition += k_ButtonSize + k_GapBetweenCells;
                    Controls.Add(cellButton);
                }

                xPosition = k_XStartLocation;
                yPosition += k_ButtonSize + k_GapBetweenCells;
            }
        }

        private void CellButtonTextChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void UpdatePlayerLabels()
        {
            for (int i = 0; i < m_PlayerNamesLabels.Length; i++) 
            {
                m_PlayerNamesLabels[i].Text = string.Format("{0}: {1}", m_Players[i].PlayerName, m_Players[i].Points);
            }
        }

        private void AddPlayersLabels()
        {
            const int heightSize = 15;
            const int widthSize = 55;
            for (int i = 0; i < m_PlayerNamesLabels.Length; i++)
            {
                m_PlayerNamesLabels[i] = new Label();
                m_PlayerNamesLabels[i].Text = string.Format("{0}: {1}", m_Players[i].PlayerName, m_Players[i].Points);
                m_PlayerNamesLabels[i].AutoSize = true;
                m_PlayerNamesLabels[i].Size = new Size(widthSize, heightSize);
                if (i == 0) 
                {
                    m_PlayerNamesLabels[i].Location = new Point((m_CurrentXSize / 2) - (m_CurrentXSize / 4), m_CurrentYSize - (k_ButtonSize / 2));
                }
                else
                {
                    m_PlayerNamesLabels[i].Location = new Point(m_CurrentXSize / 2, m_CurrentYSize - (k_ButtonSize / 2));
                }

                Controls.Add(m_PlayerNamesLabels[i]);
            }

            m_CurrentYSize += k_ButtonSize;              
        }

        private void initClickAbleButtons()
        {
            int xLocation = k_XStartLocation;
            m_ClickAbleButtons = new List<Button>();
            for (int currColNum = 0; currColNum < m_BoardLogic.NumOfCols; currColNum++)
            {
                Button smallCellButton = new Button();
                smallCellButton.Name = string.Format("clickAbleCell{0}", currColNum);
                smallCellButton.Size = new Size(k_ButtonSize, k_ButtonSize / 2);
                smallCellButton.Location = new Point(xLocation, k_YStartLocation);
                smallCellButton.Text = string.Format("{0}", currColNum + 1);
                smallCellButton.TextAlign = ContentAlignment.MiddleCenter;
                smallCellButton.Click += CellButtonClickAble;
                m_ClickAbleButtons.Add(smallCellButton);
                xLocation += k_ButtonSize + k_GapBetweenCells;
                Controls.Add(smallCellButton);             
            }
        }

        private void CellButtonClickAble(object sender, EventArgs e)
        {
            int colToAddSign = 0;
            int.TryParse(((Button)sender).Text, out colToAddSign);
            DialogResult messageBoxResult = DialogResult.None;
            if (m_Players[1].IsComputer)
            {
                RunComputerVSHuman(colToAddSign);
                s_NumOfMoves += 2;
            }
            else
            {
                RunHumanVSHuman(colToAddSign);
                s_NumOfMoves += 1;
            }

            if (m_Winner != 0) 
            {
                m_Players[m_Winner - 1].Points += 1;
                
                ShowWinnerMessage(ref messageBoxResult);
            }
            else if(m_BoardLogic.IsFullBoard())
            {
                ShowTieMessage(ref messageBoxResult);
            }

            if(messageBoxResult == DialogResult.No)
            {
                Close();
            }
            else if(messageBoxResult == DialogResult.Yes)
            {
                m_BoardLogic.ClearBoard();
                UpdatePlayerLabels();
                m_Winner = 0;
            }
        }

        private void ShowTieMessage(ref DialogResult io_UserChoice)
        {
            string message = string.Format("Tie!!{0}Anoter Round?", Environment.NewLine);
            io_UserChoice = MessageBox.Show(message, "A Tie!", MessageBoxButtons.YesNo);
        }

        private void ShowWinnerMessage(ref DialogResult io_UserChoice)
        {
            string message = string.Format("{0} Won!!{1}Anoter Round?", m_Players[m_Winner - 1].PlayerName, Environment.NewLine);
            io_UserChoice = MessageBox.Show(message, "A Win!", MessageBoxButtons.YesNo);
        }

        private void RunHumanVSHuman(int i_Move)
        {
            m_BoardLogic.GameBoardUpdateAndCheckIfFull(i_Move, m_Players[s_NumOfMoves % 2].Sign);
            if (m_BoardLogic.IsThereWinner(m_Players[s_NumOfMoves % 2].Sign)) 
            {
                m_Winner = (s_NumOfMoves % 2) + 1;
            }
        }

        private void RunComputerVSHuman(int i_Move)
        {
            int computerMove = 0;
            m_BoardLogic.GameBoardUpdateAndCheckIfFull(i_Move, m_Players[k_FirstPlayerTurn].Sign);
            if(m_BoardLogic.IsThereWinner(m_Players[k_FirstPlayerTurn].Sign))
            {
                m_Winner = k_FirstPlayerTurn + 1;
            }
            else
            {
                m_BoardLogic.GetMoveFromComputer(ref computerMove);
                m_BoardLogic.GameBoardUpdateAndCheckIfFull(computerMove, m_Players[k_SecondPlayerTurn].Sign);
                if (m_BoardLogic.IsThereWinner(m_Players[k_SecondPlayerTurn].Sign))
                {
                    m_Winner = k_SecondPlayerTurn + 1;
                }
            }
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
            this.Size = new Size(m_CurrentXSize, m_CurrentYSize);
            this.CenterToParent();
        }
    }
}
