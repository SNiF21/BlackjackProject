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
    public partial class Player_Select : Form
    {
        public Player_Select()
        {
            InitializeComponent();
        }
        
        private void Player_Select_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Opening_Screen openingScreen = new Opening_Screen();
            openingScreen.Show();
        }

        private void btn1player_Click(object sender, EventArgs e)
        {
            Match_Screen_1 match1 = new Match_Screen_1();
            match1.Show();
            this.Hide();
        }

        private void btn2player_Click(object sender, EventArgs e)
        {
            Match_Screen_2 match2 = new Match_Screen_2();
            match2.Show();
            this.Hide();
        }
    }
}
