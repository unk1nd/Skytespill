using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class BigIsles
    {
        private float x, y;
        private float screenheight, screenwidth;


        private Image isles2 = global::Skytespill.Properties.Resources.mountain_2;

        public Rectangle bigislesArea
        {
            get { return new Rectangle((int)this.x, (int)this.y, isles2.Width, isles2.Height); }
        }

        public BigIsles(int screenwidth, int screenheight, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
        }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(isles2, x, y, isles2.Width * 2, isles2.Height * 2);
        }
    }
}