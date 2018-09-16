using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public class UserMessages
    {
        public static void ErrorMessage(string i_Msg)
        {
            MessageBox.Show(i_Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void WinnerMessage(string i_PlayerName, ref DialogResult io_UserChoice)
        {
            string message = string.Format("{0} Won!!{1}Anoter Round?", i_PlayerName, Environment.NewLine);
            io_UserChoice = MessageBox.Show(message, "A Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static void TieMessage(ref DialogResult io_UserChoice)
        {
            string message = string.Format("Tie!!{0}Anoter Round?", Environment.NewLine);
            io_UserChoice = MessageBox.Show(message, "A Tie!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }
    }
}
