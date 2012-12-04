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
        SoundPlayer menuTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Bolt___Vodka_Polka);
        
        int selection = 0;
        public MenuPanel(Form _parent) 
        {
            this.parent = _parent;
            this.Height = parent.Height;
            this.Width = parent.Width;
            BackgroundImage = global::Skytespill.Properties.Resources.menu_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            menuTheme.PlayLooping();
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

            Image exitButton = global::Skytespill.Properties.Resources.credits_btn;
            Image exitButtonHover = global::Skytespill.Properties.Resources.credits_btn_hover;
            Image exitState = exitButton;
            
            if(this.selection == 0)
                playState = playButtonHover;
            if (this.selection == 1)
                controlsState = controlsButtonHover;
            if (this.selection == 2)
            creditsState = creditsButtonHover;
            if (this.selection == 3)
                exitState = exitButtonHover;
            
            g.DrawImage(playState, this.Width / 2 , this.Height / 4, playButton.Width, playButton.Height);


            g.DrawImage(controlsState, (this.Width - controlsButton.Width), (this.Height / 4 + controlsButton.Height - 30), controlsButton.Width, controlsButton.Height);
            g.DrawImage(creditsState, (this.Width / 2), (this.Height / 4 + creditsButton.Height / 2 * 3 + 50), creditsButton.Width, creditsButton.Height);

            g.DrawImage(exitState, (this.Width - exitButton.Width), (this.Height - exitButton.Height), exitButton.Width, exitButton.Height);




        }
        
        public void MenuPanelKeyDownEvent(object sender, KeyEventArgs e)
        {

            if (gamePanel != null)
            {
                gamePanel.game_Panel_KeyDown(this, e);
                
                if (e.KeyCode == Keys.M)
                {
                    this.Controls.Remove(gamePanel);
                    gamePanel = null;
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


                if(e.KeyCode == Keys.Up){



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
                }

                
            }
        }

        public void GamePanelKeyDownEvent(object sender, KeyEventArgs e) 
        {
            


        }
    }
}
