using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Skytespill
{
    class MenuPanel : Panel
    {
        Form parent;
        GamePanel gamePanel;
        CreditsPanel creditPanel;
        ControlsPanel controlPanel;

        SoundPlayer menuTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Bolt___Vodka_Polka);
        public int DeskH = Screen.PrimaryScreen.Bounds.Height;
        public int DeskW = Screen.PrimaryScreen.Bounds.Width;
        
        int selection = 0;
        public MenuPanel(Form _parent) 
        {
            this.parent = _parent;
            this.Size = new Size(DeskW,DeskH);
            //this.Height = DeskH;
            //this.Width = DeskW;
            //this.SetBounds(0, 0, DeskW, DeskH);
            BackgroundImage = global::Skytespill.Properties.Resources.menu_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            menuTheme.PlayLooping();
            Cursor.Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Image playButton = global::Skytespill.Properties.Resources.play_btn;
            Image playButtonHover = global::Skytespill.Properties.Resources.play_btn_hover;
            Image playState = playButton;

            Image controlsButton = global::Skytespill.Properties.Resources.controll_btn;
            Image controlsButtonHover = global::Skytespill.Properties.Resources.controll_btn_hover;
            Image controlsState = controlsButton;

            Image creditsButton = global::Skytespill.Properties.Resources.credits_btn;
            Image creditsButtonHover = global::Skytespill.Properties.Resources.credits_btn_hover;
            Image creditsState = creditsButton;

            Image exitButton = global::Skytespill.Properties.Resources.exit_btn;
            Image exitButtonHover = global::Skytespill.Properties.Resources.exit_btn_hover;
            Image exitState = exitButton;
            
            if(this.selection == 0)
                playState = playButtonHover;
            if (this.selection == 1)
                controlsState = controlsButtonHover;
            if (this.selection == 2)
            creditsState = creditsButtonHover;
            if (this.selection == 3)
                exitState = exitButtonHover;

            g.DrawImage(playState, DeskW / 2, DeskH / 4, playButton.Width, playButton.Height);


            g.DrawImage(controlsState, (DeskW - controlsButton.Width), (DeskH / 4 + controlsButton.Height - 80), controlsButton.Width, controlsButton.Height);
            g.DrawImage(creditsState, (DeskW / 2), (DeskH / 4 + creditsButton.Height / 2 * 3 ), creditsButton.Width, creditsButton.Height);

            g.DrawImage(exitState, (DeskW - exitButton.Width), (DeskH - exitButton.Height), exitButton.Width, exitButton.Height);




        }
        
        public void MenuPanelKeyDownEvent(object sender, KeyEventArgs e)
        {

            if (gamePanel != null)
            {
                gamePanel.game_Panel_KeyDown(this, e);
                
                if (e.KeyCode == Keys.Escape)
                {
                    this.Controls.Remove(gamePanel);
                    gamePanel = null;
                    menuTheme.PlayLooping();
                }
            }
            else if (creditPanel != null)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Controls.Remove(creditPanel);
                    creditPanel = null;
                    menuTheme.PlayLooping();
                }
            }
            else if (controlPanel != null)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Controls.Remove(controlPanel);
                    controlPanel = null;
                    menuTheme.PlayLooping();
                }
            }

            else
            {

                if (e.KeyCode == Keys.Escape)
                {

                    if (gamePanel != null)
                    {

                    }
                }


                if (e.KeyCode == Keys.Up)
                {



                    this.selection--;
                    if (this.selection < 0)
                        this.selection = 0;
                    Invalidate();
                }

                if (e.KeyCode == Keys.Down)
                {
                    this.selection++;
                    if (this.selection > 3)
                        this.selection = 3;
                    Invalidate();
                }

                if (e.KeyCode == Keys.Enter)
                {
                    if (this.selection == 0)
                    {
                        gamePanel = new GamePanel(this);
                        this.Controls.Add(gamePanel);
                        menuTheme.Stop();
                    }

                    if (this.selection == 3)
                    {
                        Application.Exit();
                    }
                    if (this.selection == 2)
                    {
                        creditPanel = new CreditsPanel(this);
                        this.Controls.Add(creditPanel);
                        menuTheme.Stop();
                    }
                    if (this.selection == 1)
                    {
                        controlPanel = new ControlsPanel(this);
                        this.Controls.Add(controlPanel);
                        menuTheme.Stop();
                    }
                }


            }
        }

        public void GamePanelKeyDownEvent(object sender, KeyEventArgs e) 
        {
            


        }
    }
}
