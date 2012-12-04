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
        MenuPanel menuPanel;
        public int DeskH = Screen.PrimaryScreen.Bounds.Height;
        public int DeskW = Screen.PrimaryScreen.Bounds.Width;

        public Form1()
        {
            this.menuPanel = new MenuPanel(this);
            this.menuPanel.Size = new Size(DeskW, DeskH);
            
            this.Controls.Add(menuPanel);
            InitializeComponent();
        }

        private void FormKeyDownEvent(object sender, KeyEventArgs e)
        {
                menuPanel.MenuPanelKeyDownEvent(this, e);    
        }

    }
}
