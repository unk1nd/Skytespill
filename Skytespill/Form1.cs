using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Skytespill
{
    public partial class Form1 : Form
    {
        MenuPanel menuPanel;
        GamePanel gamePanel;
        ControlsPanel controlPanel;
        CreditsPanel creditPanel;
        public int DeskH = Screen.PrimaryScreen.Bounds.Height;
        public int DeskW = Screen.PrimaryScreen.Bounds.Width;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        SoundPlayer menuTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Bolt___Vodka_Polka);
        SoundPlayer creditsTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Evan_LE_NY___Credits);

        public Form1()
        {
            this.menuPanel = new MenuPanel(this);
            this.menuPanel.Size = new Size(DeskW, DeskH);
            menuTheme.PlayLooping();
            
            this.Controls.Add(menuPanel);
            InitializeComponent();
        }

        

        private void FormKeyDownEvent(object sender, KeyEventArgs e)
        {

 
            if(menuPanel != null)
            {
                MenuKeyDownEvent(this, e);
            }
            else if (gamePanel != null)
            {
                gamePanel.game_Panel_KeyDown(this, e);
            }
            else if (controlPanel != null) 
            {

                if (e.KeyCode == Keys.Escape)
                {
                    menuPanel = new MenuPanel(this);
                    controlPanel.Hide();
                    controlPanel = null;
                    Controls.Add(menuPanel);
                    menuPanel.Show();
                    menuTheme.PlayLooping();
                }
            }
            else if (creditPanel != null)
            {

                if (e.KeyCode == Keys.Escape)
                {
                    menuPanel = new MenuPanel(this);
                    creditPanel.Hide();
                    creditPanel = null;
                    Controls.Add(menuPanel);
                    menuPanel.Show();
                    menuTheme.PlayLooping();
                }
            }
        }

        private void gameTicker(object sender, EventArgs e)
        {
            gamePanel.Invalidate(true);
        }


        private void MenuKeyDownEvent(object sender, KeyEventArgs e) 
        {

            if (e.KeyCode == Keys.Up)
            {

                menuPanel.selection--;
                if (menuPanel.selection < 0)
                    menuPanel.selection = 0;
                menuPanel.Invalidate();
            }

            if (e.KeyCode == Keys.Down)
            {
                menuPanel.selection++;
                if (menuPanel.selection > 3)
                    menuPanel.selection = 3;
                menuPanel.Invalidate();
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (menuPanel.selection == 0)
                {
                    gamePanel = new GamePanel(this);
                    menuPanel.Hide();
                    menuPanel = null;
                    Controls.Add(gamePanel);
                    gamePanel.Show();
                    t.Tick += new EventHandler(gameTicker);
                    t.Interval = 10;
                    t.Start();
                    menuTheme.Stop();

                }

                else if (menuPanel.selection == 1)
                {
                    controlPanel = new ControlsPanel(this);
                    menuPanel.Hide();
                    menuPanel = null;
                    Controls.Add(controlPanel);
                }

                else if (menuPanel.selection == 2)
                {

                    creditPanel = new CreditsPanel(this);
                    menuPanel.Hide();
                    menuPanel = null;
                    Controls.Add(creditPanel);
                    creditPanel.Show();
                    creditsTheme.PlayLooping();
                }

                else if (menuPanel.selection == 3)
                {
                    Application.Exit();
                }
               
                
            }
        }



    }
}
