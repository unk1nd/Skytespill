using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    /*
     *      Objektklasse for å håndtere Kanonkulene til NPCskipene.
     * 
     */
    class shipShot
    {
        private float x, y;
        private bool active, forward;
        private int xmax, ymax;
        private Image canonball = global::Skytespill.Properties.Resources.canonball_ship;
        private int move;


        public shipShot(float x, float y, int xmax, int ymax, int move)
        {
            this.move = move;
            this.x = x;
            this.y = y;
            this.xmax = xmax;
            this.ymax = ymax;
            this.active = true;
            this.forward = true;
        }


        public Rectangle BulletArea {
            get { return new Rectangle((int)this.x, (int)this.y, (int)15f, (int)15f); }
        }

        public Boolean Active
        {
            get { return this.active; }
            set { active = value; }
        }

        public float X {
            set { this.x = value; }
            get { return this.x; }
        }

        public float Y
        {
            set { this.y = value; }
            get { return this.y; }
        }

        public void bounce() 
        {
            if (this.forward)
                this.forward = false;
            else
                this.forward = true;
        }
        
        public void moveShot()
        {
            if (active)
            {

                float dx = 3f;

                if (!this.forward)
                {
                    dx = -dx;
                }


                if (move == 1)
                { y += dx; }
                if (move == 2)
                { x -= dx; }
                if (move == 3)
                { y -= dx; }
                if (move == 4)
                { x += dx; }
                if (move == 5)
                { 
                    y -= dx;
                    x -= dx;
                }
                if (move == 6)
                {
                    y -= dx;
                    x += dx;
                }



                //Deaktiverer seg selv hvis den ser at den er utenfor banen
                if (x < 0 || x > xmax || y < 0 || y > ymax)
                {
                    this.active = false;
                }
            }
            else {
                this.x = 9999999999;
                this.y = 9999999999;
            }
        }

        public void draw(System.Drawing.Graphics g)
        {
            if (active)
            {
                //g.FillEllipse(Brushes.Black, x - 7.5f, y - 7.5f, 15f, 15f);
                g.DrawImage(canonball, x, y , canonball.Width, canonball.Height);
            }
        }

        
    }
}
