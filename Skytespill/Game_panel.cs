using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Skytespill
{
    class game_Panel : Panel
    {
        private Form parent;  //En referanse til foreldrevinduet.
        private Image canon = global::Skytespill.Properties.Resources.canon;
        private Image castle = global::Skytespill.Properties.Resources.castle;
        private float x , y; 
        //private int canonSizeX = 39 , canonSizeY= 50;
        float rotation = 0;
        private int deskW = Screen.PrimaryScreen.Bounds.Width;
        private int deskH = Screen.PrimaryScreen.Bounds.Height;
        

        List<Shot> bullet_list = new List<Shot>();
        List<boat> boat_list = new List<boat>();
        
        public game_Panel(Form _parent)
        {
            InitializeComponent();
            parent = _parent;
            Cursor.Hide();
            //Viktig i forbindelse med animasjoner med mye bevegelse;     
            this.SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint,
                        true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Invalidate();
            
        }

        public void t_Tick()
        {
            Invalidate();
        }

        private void InitializeComponent()
        {
            
            this.SuspendLayout();
            this.KeyDown += new KeyEventHandler(this.game_Panel_KeyDown);
            this.ResumeLayout(false);
        }

        private void DrawPlayer(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.TegneGrid(g);

            x = ( deskW / 2) - (canon.Width / 2);
            y = ( deskH / 2) - (canon.Height / 2);


            //Rectangle player = new Rectangle(x / canonSizeX * canonSizeX, y / canonSizeY * canonSizeY, canonSizeX, canonSizeY);
            Rectangle player = new Rectangle(-20, -25, 40, 50);
            g.DrawImage(castle, deskW / 2 - castle.Width / 2 - canon.Width / 2, deskH / 2 - castle.Height / 2 - canon.Height / 2);

            //bullet.draw(g);
            foreach (boat b in boat_list)
            {
                b.draw(g);
                b.moveBoat();
            }
            
            
            foreach(Shot b in bullet_list)
            {
                b.draw(g);

                if (b.Active == true)
                {
                    b.moveShot();
                }   
            }

            g.TranslateTransform(x, y);
            g.RotateTransform(rotation);
            g.DrawImage(canon, player);
            g.ResetTransform();
            

            String cordinates = "Cordinates: " + x + " - " + y + "";
            Font drawFont2 = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            PointF drawPoint = new PointF(1.0F, 1.0F);
            g.DrawString(rotation.ToString(), drawFont2, drawBrush, drawPoint);
        }

        

        private void TegneGrid(Graphics g)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawPlayer(e);
            base.OnPaint(e);
        }

        private void game_Panel_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.B)
            {
                addboat();
                Invalidate();
            }

            if (e.KeyCode == Keys.D)
            {
                rotation += 5;
                if (rotation > 360)
                {
                    rotation = 0;
                }
                Invalidate();
            }
            if (e.KeyCode == Keys.A)
            {
                rotation -= 5;
                if (rotation < 0)
                {
                    rotation = 360;
                }
                Invalidate();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (bullet_list.Count <= 5)
                {
                    addbullet();
                    Invalidate();
                }
                for (int i = 0; i < bullet_list.Count; i++)
                {
                    if(bullet_list[i].Active == false)
                    {
                        bullet_list.Remove(bullet_list[i]);
                    }
                }
            }
        }

        public void keydown(KeyEventArgs e)
        {
            game_Panel_KeyDown(this, e);
        }

        public void addbullet()
        {
            bullet_list.Add(new Shot(x, y, rotation, deskW , deskH));
        }

        public void addboat()
        { 
            boat_list.Add(new boat(deskW, deskH));
        }
    }
}
