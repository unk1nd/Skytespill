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

        public int DeskH = Screen.PrimaryScreen.Bounds.Height;
        public int DeskW = Screen.PrimaryScreen.Bounds.Width;
        
        public int selection = 0;
        public MenuPanel(Form _parent) 
        {
            this.parent = _parent;
            this.Size = new Size(DeskW,DeskH);
            BackgroundImage = global::Skytespill.Properties.Resources.menu_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
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

            if (this.selection == 0)
                playState = playButtonHover;
            if (this.selection == 1)
                controlsState = controlsButtonHover;
            if (this.selection == 2)
            creditsState = creditsButtonHover;
            if (this.selection == 3)
                exitState = exitButtonHover;

            //scale høyde
            int playscaleH = (DeskH/8);
            int controlsscaleH = (DeskH / 8);
            int creditsscaleH = (DeskH / 8);
            int exitscaleH = (DeskH / 8);

            //scale lengde
            int playscaleL = (DeskW / 6);
            int controlsscaleL = (DeskW / 6);
            int creditsscaleL = (DeskW / 6);
            int exitscaleL = (DeskW / 6);

            int spaces = (DeskH / 28);

            g.DrawImage(playState, (DeskW - playscaleL - playscaleL), (DeskH / 4), playscaleL, playscaleH);
            g.DrawImage(controlsState, (DeskW - controlsscaleL - spaces - controlsscaleL / 2), (DeskH / 4 + playscaleH + spaces), controlsscaleL, controlsscaleH);
            g.DrawImage(creditsState, (DeskW - controlsscaleL - spaces), (DeskH / 4 + playscaleH + spaces + controlsscaleH + spaces), creditsscaleL, creditsscaleH);
            g.DrawImage(exitState, (DeskW - exitscaleL - spaces - exitscaleL / 4), (DeskH / 4 + playscaleH + spaces + controlsscaleH + spaces + creditsscaleH + spaces), exitscaleL, exitscaleH);    
        }

        public int Selection
        {
            get { return this.selection; }
            set { this.selection = value; }
        }
    }
}
