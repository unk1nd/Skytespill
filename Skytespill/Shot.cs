using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Skytespill
{
    class Shot
    {
        private float x, y;
        private double angle;
        private bool active;
        private int xmax, ymax;
        private Image canonball = global::Skytespill.Properties.Resources.canonball;

        public Shot(float x, float y, double a, int xmax, int ymax)
        {
            this.x = x;
            this.y = y;
            a = a - 90;
            this.angle = (a / 180) * Math.PI;
            this.xmax = xmax;
            this.ymax = ymax;
            this.active = true;
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

        public void moveShot()
        {
            if (active)
            {
                x += 7f * (float)Math.Cos(angle);
                y += 7f * (float)Math.Sin(angle);

                //Deaktiverer seg selv hvis den ser at den er utenfor banen
                if (x < 0 || x > xmax || y < 0 || y > ymax)
                {
                    this.active = false;
                }
            }
        }

        public void draw(System.Drawing.Graphics g)
        {
            if (active)
            {
                //g.FillEllipse(Brushes.Black, x - 7.5f, y - 7.5f, 15f, 15f);
                g.DrawImage(canonball, x - 7.5f, y - 7.5f, 15f, 15f);
            }
        }

        
    }
}
