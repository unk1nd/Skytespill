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
        
        public Form1()
        {

            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.menuPanel = new MenuPanel(this);
           
            this.Controls.Add(menuPanel);
            InitializeComponent();
        }

        private void FormKeyDownEvent(object sender, KeyEventArgs e)
        {
                menuPanel.MenuPanelKeyDownEvent(this, e);    
            
        }

    }
}
