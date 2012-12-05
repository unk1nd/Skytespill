using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;

namespace Skytespill
{
    class boss : RectangleConverter
    {
        private float x, y;


        // liv bilder
        private Image capt = global::Skytespill.Properties.Resources.captain_10_v2;

        //boss båt bilder
        private Image current = global::Skytespill.Properties.Resources.ship_boss_up;

        private Image boss_r = global::Skytespill.Properties.Resources.ship_boss_right;
        private Image boss_l = global::Skytespill.Properties.Resources.ship_boss_left;
        
        private int life = 9;
        private int screenwidth;
        private int screenheight;
        private int screenMargin = 50;
        private int shotPointX, shotPointY;

        private List<shipShot> shipbullet_list = new List<shipShot>();

        private Random randomX = new Random();
        private Random randomY = new Random();

        public boss(int screenwidth, int screenheight)
        {

            
            this.x = screenwidth / 2;
            this.y = screenheight;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
            this.shotPointX = randomX.Next(500, screenwidth - 500);
            this.shotPointY = randomY.Next(200, screenheight - 200);
            
        }

        public Rectangle BoatArea
        {
            get { return new Rectangle((int)this.x, (int)this.y, current.Width, current.Height); }
        }

        public int Life
        {
            get { return this.life; }
            set { life = value; }
        }

        public void moveBoat()
        {
            
            switch (currentMovement)
            {
                
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

        private Movement currentMovement = Movement.Bottom;
        
        private void rightMovement()
        {
            current = boss_r;
            //move
            if (x == this.shotPointX)
            {
                shot(3);
            }
            
            x = x + 5f;


            if (x >= screenwidth - (400 + current.Width))
                currentMovement = Movement.Left;
        }

        private void BottomMovement()
        {
            //move
            y = y - 1f;
            if (y <= screenheight - (150))
                currentMovement = Movement.Left;
        }

        private void LeftMovement()
        {
            current = boss_l;
            //move
            if (x == this.shotPointX)
            {
                shot(4);
            }
            x = x - 5f;

            if (x <= 0 + (400))
                currentMovement = Movement.Right;
        }

        private enum Movement { Right, Bottom, Left }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(current, x, y);
            g.DrawImage(capt, 20, 20, screenwidth/ 4, screenheight / 8);

            shipbullet_list.ForEach(Item =>
            {
                //WhaleHit(Item, g);
                Item.moveShot();
                Item.draw(g);
            });

        }

        public void shot(int move)
        {
            shipbullet_list.Add(new shipShot(x, y, screenwidth, screenheight, move));
        }
    }
}
