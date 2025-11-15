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
    public partial class Opening_Screen : Form
    {
        public Opening_Screen()
        {
            InitializeComponent();
            this.Resize += (s, e) => CenterButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CenterButtons()
        {
            int spacing = 20; 
            int totalHeight = btnPlay.Height + spacing + btnQuit.Height;

            int lowerHalfStart = this.ClientSize.Height / 2;
            int centerY = lowerHalfStart + (this.ClientSize.Height / 2 - totalHeight) / 2;

            btnPlay.Left = (this.ClientSize.Width - btnPlay.Width) / 2;
            btnPlay.Top = centerY;

            btnQuit.Left = (this.ClientSize.Width - btnQuit.Width) / 2;
            btnQuit.Top = btnPlay.Bottom + spacing;
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            Player_Select playerSelectForm = new Player_Select(this,this.WindowState,this.Bounds);
            playerSelectForm.Show();
            this.Hide();
        }

    }
}
