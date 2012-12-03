using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Skytespill
{
    class Player
    {
        private float x, y, rotation;
        private Image canon = global::Skytespill.Properties.Resources.canon;
        


        public Player(int screenwidth, int screenheight)
        {
            this.x = (screenwidth / 2);
            this.y = (screenheight / 2);
            this.rotation = 0;
        }

        public void draw(System.Drawing.Graphics g) {

            

            g.TranslateTransform(this.x, this.y);
            g.RotateTransform(rotation);
            g.DrawImage(canon, (0 - canon.Width / 2), (0 - canon.Height / 2), canon.Width, canon.Height);
            g.ResetTransform();
        }

        public float Rotation 
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
    }
}
