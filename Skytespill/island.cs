using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class island
    {
        private float x, y;
        
        private Image castle1 = global::Skytespill.Properties.Resources.castle_1;
        private Image castle2 = global::Skytespill.Properties.Resources.castle_2;
        private Image castle3 = global::Skytespill.Properties.Resources.castle_3;
        private int life = 3;


        public island(int screenwidth, int screenheight)
        {
            this.x = (screenwidth / 2);
            this.y = (screenheight / 2);
            
        }

        public void draw(System.Drawing.Graphics g) {

            if (life == 3)
            {
                g.DrawImage(castle1, (x) - (castle1.Width / 2), (y) - (castle1.Height / 2), castle1.Width, castle2.Height);
            }
            if (life == 2)
            {
                g.DrawImage(castle2, (x) - (castle2.Width / 2), (y) - (castle2.Height / 2), castle1.Width, castle2.Height);
            }
            if (life == 1)
            {
                g.DrawImage(castle3, (x) - (castle3.Width / 2), (y) - (castle3.Height / 2), castle1.Width, castle2.Height);
            }
        }
    }
}
