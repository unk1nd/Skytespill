using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skytespill
{
    public partial class Form1 : Form
    {
        private game_Panel gamepanel;
        public int deskHeight = Screen.PrimaryScreen.Bounds.Height;
        public int deskWidth = Screen.PrimaryScreen.Bounds.Width;

        public Form1()
        {
            InitializeComponent();
            this.Controls.Add(this.game_Panel);
            this.gamepanel = new game_Panel(this);
            gamepanel.Dock = DockStyle.Fill;
            game_Panel.Controls.Add(this.gamepanel);
            

            game_Panel.Width = deskWidth;
            game_Panel.Height = deskHeight;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gamepanel.keydown(e);
        }

        private void tick(object sender, EventArgs e)
        {
            gamepanel.t_Tick();
        }

    }
}
