using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Scott.Sprites;

namespace invader_clone
{
    class Player : Sprite
    {
        public int level = 1;
        public int score = 0;
        public int lives = 3;
        public int maxBullets = 1;
        public float bulletSpeed = 2.0f;
        //public float Speed//  inherited from Sprite class

        private Texture2D mBulletTexture;
        public static SoundEffect sndShipFiring;
        public static SoundEffect sndShipDeath;

        private Vector2 mSpawnPoint;
        private List<Rectangle> mHitBoxes = new List<Rectangle>();
        private List<Sprite> mBullets = new List<Sprite>();

        public Player(Texture2D ship, Texture2D bullet, SoundEffect shipfiring, SoundEffect shipdeath, Vector2 pos, Vector2 vel, float spdMult)
            : base(ship, pos, vel, spdMult)
        {
            mBulletTexture = bullet;
            sndShipFiring = shipfiring;
            sndShipDeath = shipdeath;

            mSpawnPoint = pos;
        }

        public List<Sprite> bullets()
        {
            return mBullets;
        }

        public void DetectCollision(List<Sprite> eBullets)
        {
            bool death = false;
            List<Sprite> toDelete = new List<Sprite>();

            this.UpdateHitboxes();

            foreach (Sprite eB in eBullets)
                foreach (Rectangle hitbox in mHitBoxes)
                    if (eB.Hitbox().Intersects(hitbox))
                    {
                        death = true;
                        toDelete.Add(eB);
                        break;
                    }
            if (death)
            {
                sndShipDeath.Play();
                lives--;
                this.setPosition(mSpawnPoint.X, mSpawnPoint.Y);
                //Clear bullets from around respawn area
                foreach (Sprite eB in eBullets)
                    if ((eB.X - eB.Width) >= (mSpawnPoint.X)
                        && eB.X <= (mSpawnPoint.X + 80))
                    {
                        if (!toDelete.Contains(eB))
                            toDelete.Add(eB);
                    }
            }

            foreach (Sprite del in toDelete)
                eBullets.Remove(del);
        }

        public bool DetectCollision(Sprite gift)
        {
            bool caught = false;

            if (gift != null)
            {
                this.UpdateHitboxes();

                foreach (Rectangle hitbox in mHitBoxes)
                    if (gift.Hitbox().Intersects(hitbox))
                        caught = true;
            }
            return caught;
        }

        public void Fire()
        {
            sndShipFiring.Play(0.5f, 0, 0);
            mBullets.Add(new Sprite(mBulletTexture,
                                    new Vector2(mPosition.X + 36, mPosition.Y - 5),
                                    new Vector2(0, -5),
                                    this.bulletSpeed));
        }

        public void UpdateBullets()
        {
            List<Sprite> toDelete = new List<Sprite>();

            foreach (Sprite b in mBullets)
            {
                b.Update();
                if (b.Y - b.Height < 0)
                    toDelete.Add(b);
            }
            foreach (Sprite del in toDelete)
                mBullets.Remove(del);
        }

        private void UpdateHitboxes()
        {
            mHitBoxes.Clear();
            mHitBoxes.Add(new Rectangle((int)mPosition.X + 37, (int)mPosition.Y, 6, 12));
            mHitBoxes.Add(new Rectangle((int)mPosition.X + 33, (int)mPosition.Y + 12, 22, 26));
            mHitBoxes.Add(new Rectangle((int)mPosition.X + 3, (int)mPosition.Y + 38, 80, 15));
            mHitBoxes.Add(new Rectangle((int)mPosition.X + 13, (int)mPosition.Y + 52, 54, 9));
            mHitBoxes.Add(new Rectangle((int)mPosition.X + 30, (int)mPosition.Y + 61, 20, 7));
        }
    }
}
