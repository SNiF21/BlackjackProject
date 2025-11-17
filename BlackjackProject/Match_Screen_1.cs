using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackjackProject
{
    public partial class Match_Screen_1 : Form
    {
        public Match_Screen_1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Player_Select playerSelect = new Player_Select();
            playerSelect.Show();
        }
    }
}
