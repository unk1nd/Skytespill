using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Skytespill
{
    class CreditsPanel : Panel
    {
        private Form parent;
        private int DeskW, DeskH;

        CreditContent cred = new CreditContent();

        public CreditsPanel(Form _parent)
        {
            
            parent = _parent;
            DeskW = parent.Width;
            DeskH = parent.Height;

            this.Width = DeskW;
            this.Height = DeskH;
            this.BackColor = System.Drawing.Color.Black;
           
            this.SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint,
                        true);
            Cursor.Hide();
        }

        private void credrole(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.TegneGrid(g);

            cred.Move();
            cred.draw(g);
            Invalidate();

        }

        private void TegneGrid(Graphics g)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            credrole(e);
            base.OnPaint(e);
        }
    }
}
