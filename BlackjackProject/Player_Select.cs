using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackjackProject
{
    public partial class Player_Select : Form
    {
        public Player_Select()
        {
            InitializeComponent();
        }

        private void Player_Select_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["Form1"].Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form mainForm = Application.OpenForms["Form1"];
            if (mainForm != null)
            {
                mainForm.Show();
            }
        }
    }
}
