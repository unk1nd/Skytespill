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
        private Image ControlImage = global::Skytespill.Properties.Resources.controls;

        
        public ControlsPanel(Form _parent)
        {
            parent = _parent;
            this.Width = parent.Width;
            this.Height = parent.Height;
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

            g.DrawImage(ControlImage, 0, 0, this.Width, this.Height);

            Invalidate();
        }
    }
}
