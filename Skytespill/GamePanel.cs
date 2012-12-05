using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Skytespill
{
    class GamePanel : Panel
    {
        private Form parent;  //En referanse til foreldrevinduet.
        private Player player;
        private island island;
        private int deskW;
        private int deskH;
        private Image capt = global::Skytespill.Properties.Resources.captain_10_v2;
        private List<Shot> bullet_list = new List<Shot>();
        private List<boat> boat_list = new List<boat>();
        private List<whale> whale_list = new List<whale>();
        private List<isles> isles_list = new List<isles>();

        

        public GamePanel(Form _parent)
        {
            
            parent = _parent;
            deskW = parent.Width;
            deskH = parent.Height;
            
            BackgroundImage = global::Skytespill.Properties.Resources.ocean_tile2;

            this.Width = deskW;
            this.Height = deskH;

            addisles();
            
            //Viktig i forbindelse med animasjoner med mye bevegelse;     
            this.SetStyle(ControlStyles.DoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint,
                        true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            
            this.player = new Player(deskW, deskH);
            this.island = new island(deskW, deskH);
            Cursor.Hide();
            InitializeComponent();
            
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


        private void addisles() 
        {
            isles_list.Add(new isles(deskW, deskH, deskW / 3, deskH / 3));
            isles_list.Add(new isles(deskW, deskH, deskW / 6, deskH / 6));
            isles_list.Add(new isles(deskW, deskH, 800 + (deskW / 2), 300 + (deskH / 2)));
            isles_list.Add(new isles(deskW, deskH, (deskW / 2) - 800, 300 + (deskH / 2)));
            isles_list.Add(new isles(deskW, deskH, 500 + (deskW / 2), deskH / 3));
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

        private void WhaleHit(whale b, Graphics g)
        {
            for (int i = 0; i < bullet_list.Count; i++)
            {

                if (b.WhaleArea.IntersectsWith(bullet_list[i].BulletArea))
                {
                    bullet_list[i].bounce();
                    
                }
            }
        }

       

        private void DrawGame(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Static images
            island.draw(g);

            //WhaleHandler
            whale_list.ForEach(Item =>
            {
                WhaleHit(Item, g);
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

            //Isles Handler
            isles_list.ForEach(Item =>
            {
                //hitCheck(Item, g);
               
                Item.draw(g);
            });


            //Player Handler
            player.draw(g);


            // TODO fikse boss skipet
            //g.DrawImage(capt,20,20,deskW/4, deskH/8);


            // TODO Algoritme for skiprunder
            // TODO Algoritmer for bossfight
            // TODO Skyting fra skip
            // TODO Legge til lyd og musikk
            
            // TODO score system
            // TODO whale respawn random
            
            // TODO spawning av skip og level design
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawGame(e);
            base.OnPaint(e);
        }

        public void game_Panel_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.D)
            {
                player.Rotation += 5;
                if (player.Rotation > 360)
                {
                    player.Rotation = 0;
                }
                //this.Invalidate();
            }
            if (e.KeyCode == Keys.A)
            {
                player.Rotation -= 5;
                if (player.Rotation < 0)
                {
                    player.Rotation = 360;
                }
                //Invalidate();
            }

            if (e.KeyCode == Keys.B)
            {
                
                ThreadStart ts = new ThreadStart(addboat);
                Thread boatThread = new Thread(ts);
                boatThread.Start();
                this.Invalidate();
            }

            

            if (e.KeyCode == Keys.N)
            {
                ThreadStart ts = new ThreadStart(addwhale);
                Thread whaleThread = new Thread(ts);
                    whaleThread.Start();
                    //this.Invalidate();
            }

            if (e.KeyCode == Keys.Space)
            {
                
                    ThreadStart ts = new ThreadStart(addbullet);
                    Thread bulletThread = new Thread(ts);
                    bulletThread.Start();
                    //this.Invalidate();
                
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
            bullet_list.Add(new Shot(player.X, player.Y, player.Rotation, deskW , deskH));
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
