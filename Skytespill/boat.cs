using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class boat : RectangleConverter
    {
        private float x, y;
        private Image current = global::Skytespill.Properties.Resources.ship_right;

        private Image ship_left = global::Skytespill.Properties.Resources.ship_left;
        private Image ship_ruined_left = global::Skytespill.Properties.Resources.ship_ruined_left;
        private Image ship_sinking_left = global::Skytespill.Properties.Resources.ship_sinking_left;

        private Image ship_right = global::Skytespill.Properties.Resources.ship_right;
        private Image ship_ruined_right = global::Skytespill.Properties.Resources.ship_ruined_right;
        private Image ship_sinking_right = global::Skytespill.Properties.Resources.ship_sinking_right;

        private Image ship_up = global::Skytespill.Properties.Resources.ship_up;
        private Image ship_ruined_up = global::Skytespill.Properties.Resources.ship_ruined_up;
        private Image ship_sinking_up = global::Skytespill.Properties.Resources.ship_sinking_up;

        private Image ship_down = global::Skytespill.Properties.Resources.ship_down;
        private Image ship_ruined_down = global::Skytespill.Properties.Resources.ship_ruined_down;
        private Image ship_sinking_down = global::Skytespill.Properties.Resources.ship_sinking_down;

        private int life = 3;
        private int screenwidth;
        private int screenheight;
        private int screenMargin = 10;
        private int shotPointX,shotPointY;

        private List<shipShot> shipbullet_list = new List<shipShot>();

        private Random randomX = new Random();
        private Random randomY = new Random();

        public boat(int screenwidth, int screenheight )
        {
            this.x = screenMargin;
            this.y = screenMargin;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
            this.shotPointX = randomX.Next(500, screenwidth - 500);
            this.shotPointY = randomY.Next(200, screenheight - 200);
        }

        public Rectangle BoatArea
        {
            get { return new Rectangle((int)this.x, (int)this.y, current.Width, current.Height ); }
        }

        public List<shipShot> Shipbullet_list
        {
            get { return this.shipbullet_list; }
           
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
            //this.shotPoint = randomX.Next(200, screenwidth - 200);
            if (x == this.shotPointX) 
            {
                shot(1);
            }

            switch(this.life)
            {
                case 3:
                 default:
                    x = x + 5f; 
                    current = ship_right;
                    break;
                case 2:
                    x = x + 2f;
                    current = ship_ruined_right;
                    break;
                case 1:
                    x = x + 1f; 
                    current = ship_sinking_right;
                    break;
            }


            if (x >= screenwidth - (screenMargin + current.Height))
            currentMovement = Movement.Right;
        }

        private void rightMovement()
        {
            //move
            if (y == this.shotPointY)
            {
                shot(2);
            }


            switch (this.life)
            {
                case 3:
                default:
                    y = y + 5f;
                    current = ship_down;
                    break;
                case 2:
                    y = y + 2f;
                    current = ship_ruined_down;
                    break;
                case 1:
                    y = y + 1f;
                    current = ship_sinking_down;
                    break;
            }

            if (y >= screenheight - (screenMargin + current.Width))
                currentMovement = Movement.Bottom;
        }

        private void BottomMovement()
        {
            //move
            if (x == this.shotPointX)
            {
                shot(3);
            }

            switch (this.life)
            {
                case 3:
                default:
                    x = x - 5f;
                    current = ship_left;
                    break;
                case 2:
                    x = x - 2f;
                    current = ship_ruined_left;
                    break;
                case 1:
                    x = x - 1f;
                    current = ship_sinking_left;
                    break;
            }

            if (x <= 0 + (screenMargin))
                currentMovement = Movement.Left;
        }

        private void LeftMovement()
        {
            //move
            if (y == this.shotPointY)
            {
                shot(4);
            }

            switch (this.life)
            {
                case 3:
                default:
                    y = y - 5f;
                    current = ship_up;
                    break;
                case 2:
                    y = y - 2f;
                    current = ship_ruined_up;
                    break;
                case 1:
                    y = y - 1f;
                    current = ship_sinking_up;
                    break;
            }

            if (y <= 0 + (screenMargin))
                currentMovement = Movement.Top;
        }

        private enum Movement { Top, Right, Bottom, Left }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(current, x, y);

            shipbullet_list.ForEach(Item =>
            {
                //WhaleHit(Item, g);
                Item.moveShot();
                Item.draw(g);
            });
            
        }

        public void shot(int move)
        {
            shipbullet_list.Add(new shipShot(x, y, screenwidth, screenheight,move));
        }
    }
}
