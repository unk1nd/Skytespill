using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class isles
    {
        private float x, y;
        private float screenheight, screenwidth;

        /*  
         * 
         *  Legger til whales
         *  
         * 
            
        */



        private Image isles1 = global::Skytespill.Properties.Resources.mountain_1;

        public Rectangle islesArea
        {
            get { return new Rectangle((int)this.x, (int)this.y, isles1.Width, isles1.Height); }
        }

        public isles(int screenwidth, int screenheight, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
        }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(isles1, x, y, isles1.Width, isles1.Height);
        }
    }
}
