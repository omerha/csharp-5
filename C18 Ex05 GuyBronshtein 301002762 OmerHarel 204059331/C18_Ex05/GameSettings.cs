using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public partial class GameSettings : Form
    {
        private FormBoard m_FormBoard;
        public GameSettings()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            if(textBoxPlayer2.Enabled)
            {
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
            }
        }
        private bool checkNamesAreFilled()
        {
            bool namesAreFilled = false;
            if (textBoxPlayer1.Text != string.Empty && textBoxPlayer2.Text != string.Empty)
            {
               namesAreFilled= true;
            }
            return namesAreFilled;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            int cols = 0, rows = 0;
            if(!checkNamesAreFilled())
            {
                MessageBox.Show("No names were entered","Error");
            }
            else
            {
                cols = decimal.ToInt32(numericUpDownCols.Value);
                rows = decimal.ToInt32(numericUpDownRows.Value);
                m_FormBoard = new FormBoard(cols, rows, textBoxPlayer1.Text, textBoxPlayer2.Text);
                m_FormBoard.ShowDialog();
                Close();
            }
        }

    }
}
