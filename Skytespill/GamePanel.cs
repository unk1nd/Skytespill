using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Media;

namespace Skytespill
{
    class GamePanel : Panel
    {
        private Form parent;  //En referanse til foreldrevinduet.
        private Player player;
        private island island;
        private int deskW;
        private int deskH;
        private int counter;
        private int level;
        Boolean bossFight = false;
        Boolean gameOver = false;
        private int score;
        
        private List<Shot> bullet_list = new List<Shot>();
        private List<boat> boat_list = new List<boat>();
        private List<whale> whale_list = new List<whale>();
        private List<isles> isles_list = new List<isles>();
        private List<BigIsles> bigisles_list = new List<BigIsles>();
        private List<boss> boss = new List<boss>();
        private List<shipShot> shipShot;
        System.Windows.Forms.Timer levelTimer = new System.Windows.Forms.Timer();
        

        public GamePanel(Form _parent)
        {
            
            parent = _parent;
            deskW = parent.Width;
            deskH = parent.Height;
            this.counter = 1;
            this.level = 10;
            this.score = 0;
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
            levelTimer.Tick += new EventHandler(boatAddHandler);
            addboat();
            levelTimer.Interval = 5000;
            levelTimer.Start();
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
            bigisles_list.Add(new BigIsles(deskW, deskH, (deskW / 2) - 800, 300 + (deskH / 2)));
            isles_list.Add(new isles(deskW, deskH, 500 + (deskW / 2), deskH / 3));
        }

        private void boatAddHandler(object sender, EventArgs e)
        { 
            addboat();
        }

        private void shipHits(List<shipShot> shipShotList)
        {

            for (int i = 0; i < shipShotList.Count; i++)
            {
                whale_list.ForEach(Item =>
                {
                    if (Item.WhaleArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].bounce();

                    }
                });
            }
            
            for (int i = 0; i < shipShotList.Count; i++)
            {
                
                    if (player.PlayerArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                        island.Life--;

                        if (island.Life <= 0)
                        {
                            this.gameOver = true;
                        }
                    }
               
                
                isles_list.ForEach(Item =>
                {
                    if (Item.islesArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                    }
                });

                isles_list.ForEach(Item =>
                {
                    if (Item.islesArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                    }
                });

                bigisles_list.ForEach(Item =>
                {
                    if (Item.bigislesArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                    }
                });

                bullet_list.ForEach(Item =>
                {
                    if (Item.BulletArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                        this.score++;
                    }
                });



            }
        }

        private void playerHits(List<Shot> bulletList, Graphics g)
        {

            bulletList.ForEach(Item =>
            {
                if (!Item.Active)
                    bulletList.Remove(Item);
            });


            for (int i = 0; i < bulletList.Count; i++)
            {
                whale_list.ForEach(Item =>
                {
                    if (Item.WhaleArea.IntersectsWith(bulletList[i].BulletArea))
                    {
                        bulletList[i].bounce();

                    }
                });
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                isles_list.ForEach(Item =>
                {
                    if (Item.islesArea.IntersectsWith(bulletList[i].BulletArea))
                    {
                        bulletList[i].Active = false;
                    }
                });

                bigisles_list.ForEach(Item =>
                {
                    if (Item.bigislesArea.IntersectsWith(bulletList[i].BulletArea))
                    {
                        bulletList[i].Active = false;
                    }
                });

                boat_list.ForEach(Item =>
                {
                    if (Item.BoatArea.IntersectsWith(bullet_list[i].BulletArea))
                    {
                        Item.Life--;
                        Explosiooon(bullet_list[i].X, bullet_list[i].Y, g);
                        bullet_list[i].Active = false;
                        this.score++;

                    }
                    if (Item.Life <= 0)
                    {
                        boat_list.Remove(Item);
                        this.counter++;

                        if (this.counter == 5)
                            this.level = 9;
                        if (this.counter == 10)
                            this.level = 8;
                        if (this.counter == 20)
                            this.level = 7;
                        if (this.counter == 30)
                            this.level = 6;
                        if (this.counter == 40)
                            this.level = 5;
                        if (this.counter == 50)
                            this.level = 4;
                        if (this.counter == 60)
                            this.level = 3;
                        if (this.counter == 70)
                            this.level = 2;
                        if (this.counter == 80)
                            this.level = 1;
                        if (this.counter == 100)
                            this.level = 0;

                    }
                });



            }
        }

        private void DrawGame(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(!this.gameOver){
            
                //Static images
                island.draw(g);

                //WhaleHandler
                whale_list.ForEach(Item =>
                {
                    Item.moveWhale();
                    Item.draw(g);
                });

                //Sjekker alle vennlige kuler sine skudd
       
                    playerHits(bullet_list, g);
           


                //Sjekker alle fiendlige båter sine skudd (Utenom Boss)
                boat_list.ForEach(Item =>
                 {
                     List<shipShot> tempList = Item.Shipbullet_list;
                     shipHits(tempList);

                     tempList.ForEach(Item2 =>
                    {
                        if (!Item2.Active)
                            tempList.Remove(Item2);
                    });
                });


                switch (this.level) 
                { 
                    case 10:
                        levelTimer.Interval = 5000;
                    break;
                    case 9:
                    levelTimer.Interval = 4000 ;
                    break;
                    case 8:
                    levelTimer.Interval = 3000 ;
                    break;
                    case 7:
                    levelTimer.Interval = 2000 ;
                    break;
                    case 6:
                    levelTimer.Interval = 1000;
                    break;
                    case 5:
                    levelTimer.Interval = 900 ;
                    break;
                    case 4:
                    levelTimer.Interval = 800;
                    break;
                    case 3:
                    levelTimer.Interval = 700;
                    break;
                    case 2:
                    levelTimer.Interval = 600;
                    break;
                    case 1:
                    levelTimer.Interval = 500;
                    break;
                    case 0:
                    levelTimer.Stop();
                    if (!bossFight)
                    {
                        boat_list.Clear();
                        boss.Add(new boss(deskW, deskH));
                        bossFight = true;
                        SoundInterface test = new SoundInterface();
                    
                    }
                    break;
                }
            

                // Boathandler
                boat_list.ForEach(Item =>
                {
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
                        Item.Y = -999999;
                    }
                    Item.draw(g);
                });

                //Isles Handler
                isles_list.ForEach(Item =>
                {
                    //hitCheck(Item, g);
               
                    Item.draw(g);
                });

                bigisles_list.ForEach(Item =>
                {
                    Item.draw(g);
                });

                //boss handler
                boss.ForEach(Item =>
                {
                    Item.moveBoat();
                    Item.draw(g);
                });

                //Player Handler
                player.draw(g);
                g.DrawString("Din Score: " + this.score + bullet_list.Count, new Font("Tahoma", 30), new SolidBrush(Color.Black), new Point(200, 260));


                // TODO fikse boss skipet
                //


            
                // TODO Algoritmer for bossfight
            
            
                // TODO score system
           
            
                // TODO spawning av skip og level design
            }
            else {
                g.DrawString("GAME OVER", new Font("Tahoma", 50), new SolidBrush(Color.Black), new Point(200, 200));
                g.DrawString("Din Score: " + this.score, new Font("Tahoma", 30), new SolidBrush(Color.Black), new Point(200, 260));
            }
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

            if (e.KeyCode == Keys.O)
            {

                ThreadStart ts = new ThreadStart(addboss);
                Thread bossThread = new Thread(ts);
                bossThread.Start();
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
                
                //if(bullet_list.Count < 5){
                    ThreadStart ts = new ThreadStart(addbullet);
                    Thread bulletThread = new Thread(ts);
                    bulletThread.Start();
                    //this.Invalidate();
               // }
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

        public void addboss()
        {
            boss.Add(new boss(deskW, deskH));
        }

        public void addwhale()
        {

            whale_list.Add(new whale(deskW, deskH));
        }

        
    }


}
