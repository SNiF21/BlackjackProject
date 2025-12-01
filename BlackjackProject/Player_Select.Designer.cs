namespace BlackjackProject
{
    partial class Player_Select
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player_Select));
            this.btnBack = new System.Windows.Forms.Button();
            this.btn1player = new System.Windows.Forms.Button();
            this.btn2player = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Indigo;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.btnBack.FlatAppearance.BorderSize = 2;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.Violet;
            this.btnBack.Location = new System.Drawing.Point(308, 342);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(183, 65);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btn1player
            // 
            this.btn1player.BackColor = System.Drawing.Color.Indigo;
            this.btn1player.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.btn1player.FlatAppearance.BorderSize = 2;
            this.btn1player.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1player.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1player.ForeColor = System.Drawing.Color.Violet;
            this.btn1player.Location = new System.Drawing.Point(71, 243);
            this.btn1player.Name = "btn1player";
            this.btn1player.Size = new System.Drawing.Size(265, 77);
            this.btn1player.TabIndex = 1;
            this.btn1player.Text = "1 Player";
            this.btn1player.UseVisualStyleBackColor = false;
            this.btn1player.Click += new System.EventHandler(this.btn1player_Click);
            // 
            // btn2player
            // 
            this.btn2player.BackColor = System.Drawing.Color.Indigo;
            this.btn2player.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.btn2player.FlatAppearance.BorderSize = 2;
            this.btn2player.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2player.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2player.ForeColor = System.Drawing.Color.Violet;
            this.btn2player.Location = new System.Drawing.Point(473, 243);
            this.btn2player.Name = "btn2player";
            this.btn2player.Size = new System.Drawing.Size(265, 77);
            this.btn2player.TabIndex = 2;
            this.btn2player.Text = "2 Players";
            this.btn2player.UseVisualStyleBackColor = false;
            // 
            // Player_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn2player);
            this.Controls.Add(this.btn1player);
            this.Controls.Add(this.btnBack);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player_Select";
            this.Text = "21 Royale";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Player_Select_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btn1player;
        private System.Windows.Forms.Button btn2player;
    }
}