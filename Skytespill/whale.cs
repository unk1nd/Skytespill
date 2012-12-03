using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class whale
    {
        private float x, y;
        private float screenheight, screenwidth;
        private Image current = global::Skytespill.Properties.Resources.whale_right;

        private Image whale_left = global::Skytespill.Properties.Resources.whale_left;
        private Image whale_right = global::Skytespill.Properties.Resources.whale_right;


        public Rectangle WhaleArea
        {
            get { return new Rectangle((int)this.x, (int)this.y, current.Width, current.Height); }
        }

        public whale(int screenwidth, int screenheight )
        {
            this.x = 500 ;
            this.y = screenheight - screenheight / 5;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
        }

        private enum Movement { Right, Left }
        private Movement currentMovement = Movement.Right;

        private void rightMovement()
        {
            current = whale_right;
            x = x + 2f;
            if (x >= screenwidth - (300 + current.Width))
                currentMovement = Movement.Left;
        }

        private void leftMovement()
        {
            current = whale_left;
            x = x - 2f;
            if (x <= 0 + (300))
                currentMovement = Movement.Right;
        }


        public void moveWhale()
        {
            switch (currentMovement)
            {
                case Movement.Right:
                    rightMovement();
                    break;
                case Movement.Left:
                    leftMovement();
                    break;
            }
            
        }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(current, x, y);
        }


    }
}
