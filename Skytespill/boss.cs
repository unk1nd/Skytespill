using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;

namespace Skytespill
{
    /*
     *      Objektklasse for å håndtere sprites Sjefen
     * 
     */
    class boss : RectangleConverter
    {
        private float x, y;


        // liv bilder
        private Image liv10 = global::Skytespill.Properties.Resources.captain_10;
        private Image liv9 = global::Skytespill.Properties.Resources.captain_9;
        private Image liv8 = global::Skytespill.Properties.Resources.captain_8;
        private Image liv7 = global::Skytespill.Properties.Resources.captain_7;
        private Image liv6 = global::Skytespill.Properties.Resources.captain_6;
        private Image liv5 = global::Skytespill.Properties.Resources.captain_5;
        private Image liv4 = global::Skytespill.Properties.Resources.captain_4;
        private Image liv3 = global::Skytespill.Properties.Resources.captain_3;
        private Image liv2 = global::Skytespill.Properties.Resources.captain_2;
        private Image liv1 = global::Skytespill.Properties.Resources.captain_1;
        private Image liv0 = global::Skytespill.Properties.Resources.captain_0;

        //boss båt bilder
        private Image current = global::Skytespill.Properties.Resources.ship_boss_up;

        private Image boss_r = global::Skytespill.Properties.Resources.ship_boss_right;
        private Image boss_l = global::Skytespill.Properties.Resources.ship_boss_left;

        private int life = 10;
        private int screenwidth;
        private int screenheight;
        private int screenMargin = 50;
        private double shotPointX, shotPointY, shotPointZ;

        public List<shipShot> shipbullet_list = new List<shipShot>();

        private Random randomX = new Random();
        private Random randomY = new Random();
        private Random randomZ = new Random();

        public boss(int screenwidth, int screenheight)
        {


            this.x = screenwidth / 2;
            this.y = screenheight;
            this.screenheight = screenheight;
            this.screenwidth = screenwidth;
            RandomShotSpots();

        }

        public Rectangle BossArea
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
                shot(5);
                shot(3);
                shot(6);
            }
            else if (x == this.shotPointY)
            {
                shot(5);
                shot(3);
                shot(6);
            }
            else if (x == this.shotPointZ)
            {
                shot(5);
                shot(3);
                shot(6);
            }
            x = x + 5f;

            if (x >= screenwidth - (screenwidth / 4.8))
            {
                RandomShotSpots();
                currentMovement = Movement.Left;
            }
        }


        private void RandomShotSpots()
        {
            this.shotPointX = randomX.Next(screenMargin, screenwidth - screenMargin);
            this.shotPointY = randomY.Next(screenMargin, screenwidth - screenMargin);
            this.shotPointZ = randomY.Next(screenMargin, screenwidth - screenMargin);
            this.shotPointX = R(this.shotPointX);
            this.shotPointY = R(this.shotPointY);
            this.shotPointZ = R(this.shotPointZ);
        }

        private void BottomMovement()
        {
            //move
            y = y - 1f;
            if (y <= screenheight - (screenheight / 7.2))
                currentMovement = Movement.Left;
        }

        private void LeftMovement()
        {
            current = boss_l;
            //move
            if (x == this.shotPointX)
            {
                shot(5);
                shot(3);
                shot(6);
            }
            else if (x == this.shotPointY)
            {
                shot(5);
                shot(3);
                shot(6);
            }
            else if (x == this.shotPointZ)
            {
                shot(5);
                shot(3);
                shot(6);
            }
            x = x - 5f;

            if (x <= 0 + (screenwidth / 4.8))
            {
                RandomShotSpots();
                currentMovement = Movement.Right;
            }
        }

        public static double R(double x)
        {
            return Math.Round(x / 5, MidpointRounding.AwayFromZero) * 5;
        }

        private enum Movement { Right, Bottom, Left }

        public void draw(System.Drawing.Graphics g)
        {
            g.DrawImage(current, x, y);

            if (life == 10)
                g.DrawImage(liv10, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 9)
                g.DrawImage(liv9, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 8)
                g.DrawImage(liv8, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 7)
                g.DrawImage(liv7, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 6)
                g.DrawImage(liv6, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 5)
                g.DrawImage(liv5, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 4)
                g.DrawImage(liv4, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 3)
                g.DrawImage(liv3, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 2)
                g.DrawImage(liv2, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 1)
                g.DrawImage(liv1, 20, 20, screenwidth / 4, screenheight / 8);
            if (life == 0)
                g.DrawImage(liv0, 20, 20, screenwidth / 4, screenheight / 8);


            shipbullet_list.ForEach(Item =>
            {
                Item.moveShot();
                Item.draw(g);
            });

        }

        public void shot(int move)
        {
            shipbullet_list.Add(new shipShot(x - 50, y, screenwidth, screenheight, move));

        }
    }
}
