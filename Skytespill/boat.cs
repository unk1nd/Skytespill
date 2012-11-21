using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class boat
    {
        private float x, y;
        private Image current = global::Skytespill.Properties.Resources.ship_right;
        private Image ship_left = global::Skytespill.Properties.Resources.ship_left;
        private Image ship_right = global::Skytespill.Properties.Resources.ship_right;
        private Image ship_up = global::Skytespill.Properties.Resources.ship_up;
        private Image ship_down = global::Skytespill.Properties.Resources.ship_down;
        private int life = 3;
        private int screenwidth;
        private int screenheight;
        private int screenMargin = 10;

        public boat(int screenwidth, int screenheight )
        {
            this.x = screenMargin;
            this.y = screenMargin;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
        }

        public int Life
        {
            get { return this.life; }
            set { life = value; }
        }

        public void moveBoat()
        {
            /*
            if (x <= screenwidth-(ship_right.Width *2 ) ) 
                { 
                    x = x + 5f; 
                    current = ship_right; 
                }
            
            else if (x >= screenwidth - (ship_right.Width * 2 -1) ) 
                { 
                    y = y + 5f; 
                    current = ship_down; 
                }
            */
            switch(currentMovement)
            {
                case Movement.Top:
                    topMovement();
                    break;
                case Movement.Right:
                    rightMovement();
                    break;
                case Movement.Bottom:
                    BottomMovement();
                    break;
                case Movement.Left:
                    LeftMovement();
                    break;
            }

        }
        private Movement currentMovement = Movement.Top;
        private void topMovement()
        {
            //move
            x = x + 5f; 
            current = ship_right;

            if (x >= screenwidth - (screenMargin + current.Height))
            currentMovement = Movement.Right;
        }

        private void rightMovement()
        {
            //move
            y = y + 5f;
            current = ship_down;

            if (y >= screenheight - (screenMargin + current.Width))
                currentMovement = Movement.Bottom;
        }

        private void BottomMovement()
        {
            //move
            x = x - 5f;
            current = ship_left;

            if (x <= 0 + (screenMargin))
                currentMovement = Movement.Left;
        }

        private void LeftMovement()
        {
            //move
            y = y - 5f;
            current = ship_up;

            if (y <= 0 + (screenMargin))
                currentMovement = Movement.Top;
        }

        private enum Movement { Top, Right, Bottom, Left }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(current, x, y);
        }
    }
}
