using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

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
        Boolean win = false;
        private int score;
        
        private List<Shot> bullet_list = new List<Shot>();
        private List<boat> boat_list = new List<boat>();
        private List<whale> whale_list = new List<whale>();
        private List<isles> isles_list = new List<isles>();
        private List<BigIsles> bigisles_list = new List<BigIsles>();
        private List<boss> sjef = new List<boss>();
        private SoundInterface music = new SoundInterface();
        System.Windows.Forms.Timer levelTimer = new System.Windows.Forms.Timer();
        ThreadStart ts;
        Thread whaleThread, boatThread, obstacleThread;
        
        public GamePanel(Form _parent)
        {
            // Dobbelbuffering og litt annet snacks for å forhindre at skjermen flikrer.
            this.SetStyle(ControlStyles.DoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint,
            true);
            this.UpdateStyles();
            this.SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Cursor.Hide(); // Skjuler musepekeren


            this.BackgroundImage = global::Skytespill.Properties.Resources.ocean_tile2;  //Setter bakgrunnsbilde for spillet.

            this.parent = _parent;           //Setter foreldreobjektet
            this.deskW = parent.Width;       //Definerer størrelsen til foreldreobjektet
            this.deskH = parent.Height;      //Definerer størrelsen til foreldreobjektet
            this.Width = deskW;         //strekker panelet til foreldreobjektet
            this.Height = deskH;        //strekker panelet til foreldreobjektet
            this.counter = 1;           //Definerer counter for bruk i regi av levels.
            this.level = 10;            //Definerer antall levels før bossen.
            this.score = 0;             //Definerer scoren.
            
            this.player = new Player(deskW, deskH);     //Deklarerer spillerobjektet
            this.island = new island(deskW, deskH);     //Deklarerer Spillerobjektets øy

            //Legger trådete båter i en timer, og starter den.
            levelTimer.Tick += new EventHandler(boatAddHandler);
            levelTimer.Interval = 5000;
            levelTimer.Start();

            //Legger hval i tråd
            ts = new ThreadStart(addwhale);
            whaleThread = new Thread(ts);
            whaleThread.Start();

            //Legger til hindringer i tråd
            ts = new ThreadStart(AddObstacles);
            obstacleThread = new Thread(ts);
            obstacleThread.Start();
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            //Så lenge spiller IKKE er over.
            if (!this.gameOver)
            {
                island.draw(g);                 //Legger inn øyen til spilleren


                
                playerHits(bullet_list, g);     //Sjekker om spilleren sine skudd har truffet noe

                //Sjekker om båtene har truffet noe.
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

                //Sjekker om bossen har truffet noe
                sjef.ForEach(Item =>
                {
                    List<shipShot> tempList = Item.shipbullet_list;
                    shipHits(tempList);

                    tempList.ForEach(Item2 =>
                    {
                        if (!Item2.Active)
                            tempList.Remove(Item2);
                    });
                });
                
                
                //Oppdatering og tegning av hval
                whale_list.ForEach(Item =>
                {
                    Item.moveWhale();
                    Item.draw(g);
                });

                //Oppdatering og tekning av Båter
                boat_list.ForEach(Item =>
                {
                    Item.moveBoat();
                    Item.draw(g);
                });

                //Oppdatering og tegning av Spilleren sine kuler
                bullet_list.ForEach(Item =>
                {
                    if (Item.Active == true)
                    {
                        Item.moveShot();
                    }
                    Item.draw(g);
                });

                //Tegning av Små øyer
                isles_list.ForEach(Item =>
                {
                    Item.draw(g);
                });

                //Tegning av Store øyer
                bigisles_list.ForEach(Item =>
                {
                    Item.draw(g);
                });

                //Oppdatering og tegning av Boss
                sjef.ForEach(Item =>
                {
                    Item.moveBoat();
                    Item.draw(g);
                });

                //Tegning av Player (Oppdatering skjer i keyhandlers)
                player.draw(g);

                //Legger til Score
                g.DrawString("Score: " + this.score, new Font("Tahoma", 20), new SolidBrush(Color.Black), new Point(0, deskH - 50));

                //Siden vi vil være snille mot spillerne, og ikke la dem få minuspoeng i score.
                if (this.score < 0)
                    this.score = 0;
            }
            else //Når spillet er over
            {
                if (!win) //OM man ikke vant, så får man bestkjed om at man tapte
                    g.DrawString("DU TAPTE...", new Font("Tahoma", 50), new SolidBrush(Color.Black), new Point(200, 200));
                else //Ellers så får man en gratulasjon.
                    g.DrawString("DU VANT!", new Font("Tahoma", 50), new SolidBrush(Color.Black), new Point(200, 200));

                //Viser Score, og veildening på hvordan man kommer seg tilbake til menyen.
                g.DrawString("Din Score: " + this.score, new Font("Tahoma", 30), new SolidBrush(Color.Black), new Point(200, 300));
                g.DrawString("Trykk Esc eller Escape for å komme tilbake til menyen", new Font("Tahoma", 25), new SolidBrush(Color.Black), new Point(200, 350));
            }            
        }

        //Håndterer Threads for båtene
        private void boatAddHandler(object sender, EventArgs e)
        {
            ts = new ThreadStart(addboat);
            boatThread = new Thread(ts);
            boatThread.Start();

            switch (this.level)
            {
                case 10:
                    levelTimer.Interval = 5000;
                    break;
                case 9:
                    levelTimer.Interval = 4000;
                    break;
                case 8:
                    levelTimer.Interval = 3000;
                    break;
                case 7:
                    levelTimer.Interval = 2000;
                    break;
                case 6:
                    levelTimer.Interval = 1000;
                    break;
                case 5:
                    levelTimer.Interval = 900;
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
                        sjef.Add(new boss(deskW, deskH));
                        bossFight = true;
                        music.PlayBossMusic();

                    }
                    break;
            }
        }

        //Metode for lett å legge inn hindringer.
        private void AddObstacles()
        {
            isles_list.Add(new isles(deskW, deskH, deskW / 3, deskH / 3));
            isles_list.Add(new isles(deskW, deskH, deskW / 6, deskH / 6));
            isles_list.Add(new isles(deskW, deskH, 800 + (deskW / 2), 300 + (deskH / 2)));
            bigisles_list.Add(new BigIsles(deskW, deskH, (deskW / 2) - 800, 300 + (deskH / 2)));
            isles_list.Add(new isles(deskW, deskH, 500 + (deskW / 2), deskH / 3));
        }
        
        // Tegner eksplosjonen
        public void DrawExplosion(float x, float y, Graphics g)
        {
            Image current = global::Skytespill.Properties.Resources.explosion_1;
            Image explo1 = global::Skytespill.Properties.Resources.explosion_1;
            Image explo2 = global::Skytespill.Properties.Resources.explosion_2;
            Image explo3 = global::Skytespill.Properties.Resources.explosion_3;

            g.DrawImage(explo2, x, y);   
        }
       
        //Metode for å sjekke om fiendens kanonkuler har truffet noe.
        private void shipHits(List<shipShot> shipShotList)
        {
            //Sjekker om noen kanonkuler skal sprette tilbake
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
            //Sjekker om noen kanonkuler skal fjernes/destrueres
            for (int i = 0; i < shipShotList.Count; i++)
            {
                    //Kollisjonsdetektor for spilleren. Her mister spilleren liv om han blir truffet.
                    if (player.PlayerArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                        island.Life--;

                        if (island.Life <= 0)
                        {
                            this.gameOver = true;
                        }
                    }
               
                //Kuler vs  små øyer
                isles_list.ForEach(Item =>
                {
                    if (Item.islesArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                    }
                });

                //Kuler vs store øyer
                bigisles_list.ForEach(Item =>
                {
                    if (Item.bigislesArea.IntersectsWith(shipShotList[i].BulletArea))
                    {
                        shipShotList[i].Active = false;
                    }
                });

                //Kuler vs spillers kuler
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

        //Metode for å sjekke om Spiller sine kuler har truffet noe
        private void playerHits(List<Shot> bulletList, Graphics g)
        {

            //Sjekker etter potensielle unødvendige kuler som ikke er innenfor spillebrettet
            bulletList.ForEach(Item =>
            {
                //Deaktiverer seg selv hvis den ser at den er utenfor banen
                if (Item.X < 0 || Item.X > deskW || Item.Y < 0 || Item.Y > deskH)
                {
                    Item.Active = false;
                    this.score--;
                }
                
                if (!Item.Active)
                    bulletList.Remove(Item);
            });

            //Sjekker først etter om kulene skal bounce.
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

            //Sjekker om kulene skal destrueres
            for (int i = 0; i < bulletList.Count; i++)
            {
                //Kuler vs Små øyer
                isles_list.ForEach(Item =>
                {
                    if (Item.islesArea.IntersectsWith(bulletList[i].BulletArea))
                    {
                        bulletList[i].Active = false;
                    }
                });

                //Kuler vs Store øyer
                bigisles_list.ForEach(Item =>
                {
                    if (Item.bigislesArea.IntersectsWith(bulletList[i].BulletArea))
                    {
                        bulletList[i].Active = false;
                    }
                });

                //Kuler vs båter
                boat_list.ForEach(Item =>
                {
                    if (Item.BoatArea.IntersectsWith(bullet_list[i].BulletArea))
                    {
                        Item.Life--;
                        DrawExplosion(bullet_list[i].X, bullet_list[i].Y, g);
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

                // Kuler VS Boss-skiper
                sjef.ForEach(Item =>
                {
                    if (Item.BossArea.IntersectsWith(bullet_list[i].BulletArea))
                    {
                        Item.Life--;
                        DrawExplosion(bullet_list[i].X, bullet_list[i].Y, g);
                        bullet_list[i].Active = false;
                        this.score++;

                    }
                    if (Item.Life <= 0)
                    {
                        this.win = true;
                        this.gameOver = true;
                        sjef.Remove(Item);
                        this.counter++;
                    }
                });

            }
        }

        //Keydowneventet
        public void keydown(object sender, KeyEventArgs e)
        {
            //Til bruk for å teste diverse objekter (Se metode nederst)
            //KeyDownDEBUG(this, e);

            //Roterer spiller til venstre
            if (e.KeyCode == Keys.D)
            {
                player.Rotation += 5;
                
                if (player.Rotation > 360)
                    player.Rotation = 0;
            }

            //Roterer spiller til høyre
            if (e.KeyCode == Keys.A)
            {
                player.Rotation -= 5;
                
                if (player.Rotation < 0)
                    player.Rotation = 360;
            }
            
            //Skyter kanonkule
            if (e.KeyCode == Keys.Space)
            {
                if (bullet_list.Count < 5)
                {
                    ThreadStart ts = new ThreadStart(addbullet);
                    Thread bulletThread = new Thread(ts);
                    bulletThread.Start();
                }
            }
        }

        //Metode brukt av threadhandler for å legge til kuler
        public void addbullet()
        {
            bullet_list.Add(new Shot(player.X, player.Y, player.Rotation, deskW , deskH));
        }

        //Metode brukt av threadhandler for å legge til båter
        public void addboat()
        { 
            boat_list.Add(new boat(deskW, deskH));
        }

        //Metode brukt av threadhandler for å legge til sjefsbåten
        public void addboss()
        {
            sjef.Add(new boss(deskW, deskH));
        }

        //Metode brukt av threadhandler for å legge til hval
        public void addwhale()
        {
            whale_list.Add(new whale(deskW, deskH));
        }

        /* Debugkode. Kan avkommenteres sammen med metoden i keydown 
         * handleren for å få tilgang til å legge ut hindere og båter.
        public void KeyDownDEBUG(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B)
            {
                ThreadStart ts = new ThreadStart(addboat);
                Thread boatThread = new Thread(ts);
                boatThread.Start();
            }

            if (e.KeyCode == Keys.O)
            {

                ThreadStart ts = new ThreadStart(addboss);
                Thread bossThread = new Thread(ts);
                bossThread.Start();
            }

            if (e.KeyCode == Keys.N)
            {
                ThreadStart ts = new ThreadStart(addwhale);
                Thread whaleThread = new Thread(ts);
                whaleThread.Start();
            }
    
        }
        */
         
    }
}