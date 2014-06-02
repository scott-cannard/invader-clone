using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Scott.CollisionDetection;
using Scott.Sprites;
using Scott.Utilities;

namespace invader_clone
{
    class AlienManager
    {
        private Texture2D[] mAlienTexture = new Texture2D[4];
        private Texture2D mBulletTexture;
        public static SoundEffect sndAlienFiring;
        public static SoundEffect sndAlienDeath;

        private List<Sprite> mAliens = new List<Sprite>();
        private List<Sprite> mBullets = new List<Sprite>();
        private Random rand = new Random();

        public AlienManager(Texture2D alien1, Texture2D alien2, Texture2D alien3, Texture2D alien4, Texture2D bullet, SoundEffect alienfiring, SoundEffect aliendeath)
        {
            mAlienTexture[0] = alien1;
            mAlienTexture[1] = alien2;
            mAlienTexture[2] = alien3;
            mAlienTexture[3] = alien4;
            mBulletTexture = bullet;
            sndAlienFiring = alienfiring;
            sndAlienDeath = aliendeath;
        }

        public List<Sprite> alienList()
        {
            return mAliens;
        }

        public List<Sprite> bullets()
        {
            return mBullets;
        }

        public void Clear()
        {
            mAliens.Clear();
            mBullets.Clear();
        }

        public int count()
        {
            return mAliens.Count;
        }

        public void DetectCollision(List<Sprite> pBullets, Player player)
        {
            //***** This routine is not safe!!
            //***** If 2 objects in either list collide at the same time
            //***** the program will crash!
            List<Sprite> aDelete = new List<Sprite>();
            List<Sprite> pBDelete = new List<Sprite>();

            foreach (Sprite a in mAliens)
                foreach (Sprite pB in pBullets)
                    if (a.Hitbox().Intersects(pB.Hitbox()))
                    {
                        sndAlienDeath.Play();
                        player.score += 50 * player.level;
                        aDelete.Add(a);
                        pBDelete.Add(pB);
                    }
            foreach (Sprite del in aDelete)
                mAliens.Remove(del);
            foreach (Sprite del in pBDelete)
                pBullets.Remove(del);
        }

        public void Fire(int level)
        {
            foreach (Sprite a in mAliens)
            {
                if (mBullets.Count < 30 && rand.Next(25000) < Math.Pow(level, 2))
                {
                    sndAlienFiring.Play();
                    mBullets.Add(new Sprite(mBulletTexture,
                                            new Vector2(a.X + 13, a.Y + 40),
                                            new Vector2(0, 2.0f),
                                            1.0f + (float)level / 10.0f));
                }
            }
        }

        public void Spawn(int level)
        {
            mAliens.Clear();
            for (int a = 0; a < 40; a++)
            {
                mAliens.Add(new Sprite(mAlienTexture[a / 10],
                                       new Vector2(40 + (int)(a % 10) * 75, 50 + (int)(4 - (a / 10)) * 70),
                                       new Vector2(level + 2, 0),
                                       0.2f));
            }
        }

        public bool Update(GraphicsDevice gDev, int bottom)
        {
            bool reachedEdge = false;
            bool gameover = false;

            //Check for arrival at edge of screen
            foreach (Sprite a in mAliens)
            {
                a.Update();
                if ((a.X >= (gDev.Viewport.Width - 200) - a.Width) || (a.X <= 0))
                    reachedEdge = true;
            }
            //Bounce and drop down
            if (reachedEdge)
                foreach (Sprite a in mAliens)
                {
                    a.HorizontalBounce();
                    a.setPosition(a.X, a.Y + a.Height);
                    //Check for player death
                    if ((a.Y + a.Height) > bottom)
                        gameover = true;
                }
            List<Sprite> toDelete = new List<Sprite>();
            foreach (Sprite b in mBullets)
            {
                b.Update();
                if ((b.Y + b.Height) > gDev.Viewport.Height)
                    toDelete.Add(b);
            }
            foreach (Sprite del in toDelete)
                mBullets.Remove(del);

            return (!gameover);
        }
    }
}
