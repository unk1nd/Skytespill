using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Skytespill
{
    /*
     *      Panel for å vise rullende Credits.
     * 
     */
    class CreditsPanel : Panel
    {
        private Form parent;
        private Image castle1 = global::Skytespill.Properties.Resources.credits;
        private float x, y;

        public CreditsPanel(Form _parent)
        { 
            parent = _parent;
            this.Width = parent.Width;
            this.Height = parent.Height;

            this.y = Height;
            this.x = 0;

            this.BackColor = System.Drawing.Color.Black;
           
            this.SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint,
                        true);
            Cursor.Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(castle1, x, y, this.Width, this.Height * 3);
            this.y -= 0.9f; 
            
            Invalidate();
        }
    }
}
