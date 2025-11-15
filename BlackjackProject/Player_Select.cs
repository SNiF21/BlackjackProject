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
        private Form mainForm;
        public Player_Select(Form opener, FormWindowState state, Rectangle bounds)
        {
            InitializeComponent();
            this.Resize += (s, e) => AlignPlayerSelect();
            mainForm = opener;
            this.WindowState = state;
            if (state != FormWindowState.Maximized)
            {
                this.Bounds = bounds;
            }
        }
        private void AlignPlayerSelect()
        {
            btn1player.Width = 170;
            btn2player.Width = 170;


            int horizontalSpacing = 40;
            int buttonHeight = btn1player.Height;

            int lowerHalfStart = this.ClientSize.Height / 2;
            int totalButtonsHeight = buttonHeight + 20 + btnBack.Height;

            int topRowY = lowerHalfStart + (this.ClientSize.Height / 2 - totalButtonsHeight) / 2;

            int centerX = this.ClientSize.Width / 2;

            btn1player.Top = topRowY;
            btn1player.Left = centerX - btn1player.Width - horizontalSpacing / 2;

            btn2player.Top = topRowY;
            btn2player.Left = centerX + horizontalSpacing / 2;

            btnBack.Left = centerX - btnBack.Width / 2;
            btnBack.Top = btn1player.Bottom + 20;
        }
        private void Player_Select_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (mainForm != null)
            {
                mainForm.Show();
            }
        }
    }
}
