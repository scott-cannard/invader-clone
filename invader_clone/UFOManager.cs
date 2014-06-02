using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Scott.Sprites;
using Scott.Utilities;

namespace invader_clone
{
    enum GiftType { SCORE, LIFE, SHIPSPD, BULLETS, BULLETSPD };

    class UFOManager
    {
        private Texture2D mUFOTexture;
        private Texture2D mGiftboxTexture;
        public static SoundEffect sndUFOMoving;
        public static SoundEffect sndGiftboxDrop;
        public static SoundEffect sndGiftboxCollect;

        private Sprite mUfo = null;
        public int mTimer = 0;

        private bool mDropping = false;
        private bool mCatching = false;
        private int mCatchFrame = 1;
        private GiftType giftType;

        private Random rand = new Random();

        public UFOManager(Texture2D bigalien, Texture2D giftbox, SoundEffect ufomoving, SoundEffect giftboxdrop, SoundEffect giftboxcollect)
        {
            mUFOTexture = bigalien;
            mGiftboxTexture = giftbox;
            sndUFOMoving = ufomoving;
            sndGiftboxDrop = giftboxdrop;
            sndGiftboxCollect = giftboxcollect;
        }

        public void CatchGift(Player player)
        {
            sndGiftboxCollect.Play();
            float hyp;
            mDropping = false;
            mCatching = true;
            //Choose random reward
            switch (rand.Next(1000) % 9)
            {
                case 8:
                    hyp = 0.05f * (float)Math.Sqrt(Math.Pow(1180 - mUfo.X, 2) + Math.Pow(235 - mUfo.Y, 2));
                    mUfo.setVelocity((1180 - mUfo.X) / hyp, (235 - mUfo.Y) / hyp);
                    giftType = GiftType.LIFE;             
                    break;
                case 7:
                case 6:
                    if (player.bulletSpeed < 4)
                    {
                        hyp = 0.05f * (float)Math.Sqrt(Math.Pow(1180 - mUfo.X, 2) + Math.Pow(625 - mUfo.Y, 2));
                        mUfo.setVelocity((1180 - mUfo.X) / hyp, (625 - mUfo.Y) / hyp);
                        giftType = GiftType.BULLETSPD;
                        break;
                    }
                    else
                        goto default;
                case 5:
                case 4:
                    if (player.maxBullets < 8)
                    {
                        hyp = 0.05f * (float)Math.Sqrt(Math.Pow(1180 - mUfo.X, 2) + Math.Pow(485 - mUfo.Y, 2));
                        mUfo.setVelocity((1180 - mUfo.X) / hyp, (485 - mUfo.Y) / hyp);
                        giftType = GiftType.BULLETS;
                        break;
                    }
                    else
                        goto default;
                case 3:
                case 2:
                case 1:
                    if (player.Speed < 10)
                    {
                        hyp = 0.05f * (float)Math.Sqrt(Math.Pow(1180 - mUfo.X, 2) + Math.Pow(350 - mUfo.Y, 2));
                        mUfo.setVelocity((1180 - mUfo.X) / hyp, (350 - mUfo.Y) / hyp);
                        giftType = GiftType.SHIPSPD;
                        break;
                    }
                    else
                        goto default;
                default://case (0)
                    hyp = 0.05f * (float)Math.Sqrt(Math.Pow(1180 - mUfo.X, 2) + Math.Pow(120 - mUfo.Y, 2));
                    mUfo.setVelocity((1180 - mUfo.X) / hyp, (120 - mUfo.Y) / hyp);
                    giftType = GiftType.SCORE;
                    break;
            }
        }

        public bool Caught()
        {
            return mCatching;
        }

        public void Destroy()
        {
            mUfo = null;
            mDropping = false;
            mCatching = false;
            mCatchFrame = 1;
        }

        public void DetectCollision(List<Sprite> bullets, int level)
        {
            if (mUfo != null && !mDropping && !mCatching)
            {
                Sprite del = null;

                foreach (Sprite b in bullets)
                {
                    if (b.Hitbox().Intersects(mUfo.Hitbox()))
                    {
                        del = b;
                        break;
                    }
                }
                if (del != null)
                {
                    sndGiftboxDrop.Play();
                    bullets.Remove(del);
                    DropGift(level);
                }
            }
        }

        public void Draw(SpriteBatch sprBatch)
        {
            if (mUfo != null)
            {
                if (!mDropping && !mCatching && mUfo.getVelocity().X > 0)
                {
                    mUfo.DrawFlipped(sprBatch, 1, 1);
                }
                else if (mCatching)
                    mUfo.Draw(sprBatch, 10.0f / ((float)mCatchFrame + 9), 10.0f / ((float)mCatchFrame + 9));
                else
                    mUfo.Draw(sprBatch);
            }
        }

        public void DropGift(int level)
        {
            mDropping = true;
            mUfo.ReloadImage(mGiftboxTexture);
            mUfo.setPosition(mUfo.X + 20, mUfo.Y + 30);
            mUfo.setVelocity(0, Math.Abs(mUfo.getVelocity().X));
        }
 
        public Sprite Gift()
        {
            return mUfo;
        }

        public void Spawn(GraphicsDevice gDev, int level)
        {
            if (mUfo == null && mTimer > (750 - (level * 5))
                && rand.Next(SCUtil.Max(0, 2250 - SCUtil.Min(mTimer, 2249))) < 1)   //<-- 1/1000 chance per cycle
            {
                sndUFOMoving.Play();
                if (rand.Next(10) < 5)
                {
                    mUfo = new Sprite(mUFOTexture,
                                      new Vector2(gDev.Viewport.Width - 200, 20),
                                      new Vector2(-1.5f, 0),
                                      1.0f + (float)level / 8.0f);
                }
                else
                {
                    mUfo = new Sprite(mUFOTexture,
                                      new Vector2(-68, 20),
                                      new Vector2(1.5f, 0),
                                      1.0f + (float)level / 8.0f);
                }
            }
        }

        public void SplashCreate(GraphicsDevice gDev)
        {
            mUfo = new Sprite(mUFOTexture,
                              new Vector2(gDev.Viewport.Width / 2, gDev.Viewport.Height * 0.54f),
                              new Vector2(3, 0));
        }

        public void SplashDance(GraphicsDevice gDev)
        {
            if (mUfo != null)
            {
                mUfo.Update();
                if ((mUfo.X + mUfo.Width / 2) > (gDev.Viewport.Width * 0.8f - mUfo.Width)
                    || (mUfo.X + mUfo.Width / 2) < (gDev.Viewport.Width * 0.2f))
                    //center of sprite is past 20% of screen width
                    mUfo.HorizontalBounce();
            }
        }

        public void SplashDraw(SpriteBatch sprBatch, GraphicsDevice gDev)
        {
            if (mUfo != null)
            {
                if (mUfo.getVelocity().X < 0)
                    mUfo.Draw(sprBatch, 2, 2);
                else
                    mUfo.DrawFlipped(sprBatch, 2, 2);
            }
        }

        public void Update(GraphicsDevice gDev, Player player)
        {
            if (mUfo != null)
            {
                if (mCatching)
                    mCatchFrame++;
                mUfo.Update();

                if (!mDropping && !mCatching && (mUfo.X < -mUfo.Width || mUfo.X > (gDev.Viewport.Width - 200))
                    || (mDropping && mUfo.Y > gDev.Viewport.Height))
                {
                    mTimer = 0;
                    this.Destroy();
                }
                else if (mCatching && mUfo.X >= 1180)
                {
                    mTimer = 0;
                    this.Destroy();
                    switch (giftType)
                    {
                        case GiftType.SCORE:
                            player.score += 10000 * player.level;
                            break;

                        case GiftType.LIFE:
                            player.lives++;
                            break;

                        case GiftType.SHIPSPD:
                            player.setSpeedMultiplier(player.Speed + 0.5f);
                            break;

                        case GiftType.BULLETS:
                            player.maxBullets++;
                            break;

                        case GiftType.BULLETSPD:
                            player.bulletSpeed += 0.5f;
                            break;
                    }
                }
            }
            else
            {
                mTimer++;
            }
        }
    }
}
