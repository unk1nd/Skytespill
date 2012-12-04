using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Skytespill
{
    class CreditContent
    {
        private float x, y;

        public int DeskH = Screen.PrimaryScreen.Bounds.Height;
        public int DeskW = Screen.PrimaryScreen.Bounds.Width;
        
        private Image castle1 = global::Skytespill.Properties.Resources.credit_test;
        

        public CreditContent()
        {
            this.y = DeskH;
            this.x = 0;
            
        }
        public void Move() { this.y -= 0.3f; }

        public void draw(System.Drawing.Graphics g) {

                g.DrawImage(castle1, x, y, DeskW, DeskH*3);
            
        }
    }
}
