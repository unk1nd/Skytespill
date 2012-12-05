using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Skytespill
{
    class ControlsPanel : Panel
    {
        private Form parent;
        private int DeskW, DeskH;
        private Image ControlImage = global::Skytespill.Properties.Resources.controls;

        public ControlsPanel(Form _parent)
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

        private void ControllDraw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.TegneGrid(g);

            g.DrawImage(ControlImage, 0, 0, DeskW, DeskH);
            
            Invalidate();

        }

        private void TegneGrid(Graphics g)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ControllDraw(e);
            base.OnPaint(e);
        }
    }
}
