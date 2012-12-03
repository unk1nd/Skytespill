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
        private Image player_life = global::Skytespill.Properties.Resources.life_0hit;
        private float x , y; 
        //private int canonSizeX = 39 , canonSizeY= 50;
        float rotation = 0;
        private int deskW = Screen.PrimaryScreen.Bounds.Width;
        private int deskH = Screen.PrimaryScreen.Bounds.Height;

        

        List<Shot> bullet_list = new List<Shot>();
        List<boat> boat_list = new List<boat>();
        List<whale> whale_list = new List<whale>();
        
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


        public void Explosiooon(float x, float y, Graphics g)
        {
            
            Image current = global::Skytespill.Properties.Resources.explosion_1;
            Image explo1 = global::Skytespill.Properties.Resources.explosion_1;
            Image explo2 = global::Skytespill.Properties.Resources.explosion_2;
            Image explo3 = global::Skytespill.Properties.Resources.explosion_3;

            g.DrawImage(explo2, x, y);   
        }
       
       


        private void InitializeComponent()
        {
            
            this.SuspendLayout();
            this.KeyDown += new KeyEventHandler(this.game_Panel_KeyDown);
            this.ResumeLayout(false);
        }

        private void hitCheck(boat b, Graphics g)
        {
            for (int i = 0; i < bullet_list.Count; i++)
            {

                if (b.BoatArea.IntersectsWith(bullet_list[i].BulletArea))
                {
                    b.Life--;
                    Explosiooon(bullet_list[i].X, bullet_list[i].Y, g);
                    bullet_list[i].Active = false;
                }
                if(b.Life <= 0) {
                    boat_list.Remove(b);
                    
                }
            }
        }

       

        private void DrawPlayer(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.TegneGrid(g);

            g.DrawImage(castle, deskW / 2 - castle.Width / 2 - canon.Width / 2, deskH / 2 - castle.Height / 2 - canon.Height / 2);

            //WhaleHandler
            whale_list.ForEach(Item =>
            {
                Item.moveWhale();
                Item.draw(g);
            });

            // Boathandler
            boat_list.ForEach(Item =>
            {
                hitCheck(Item, g);
                Item.moveBoat();
                Item.draw(g);
            });

            //Bullethandler
            bullet_list.ForEach(Item =>
            {
                if (Item.Active == true)
                {
                    Item.moveShot();
                }
                else
                {
                    Item.X = -999999;
                }
                Item.draw(g);
            });

            //Player Handler

            //Finner center for Canon bilde.
            x = ( deskW / 2);
            y = ( deskH / 2);


            Rectangle player = new Rectangle(-20, -25, 40, 50);
            


            g.TranslateTransform(x, y);
            g.RotateTransform(rotation);
            g.DrawImage(canon, player);
            g.ResetTransform();
            g.DrawImage(player_life, deskW - 46, 1);

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
            if (e.KeyCode == Keys.N)
            {
                    addwhale();
                    Invalidate();
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

        public void addwhale()
        {
            whale_list.Add(new whale(deskW, deskH));
        }

        
    }


}
