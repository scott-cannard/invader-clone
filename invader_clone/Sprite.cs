using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
using Scott.Utilities;

namespace Scott.Sprites
{
    class Sprite    //***Superclass***
    {
        //Quick-access members
        public int X;           // mPosition.X
        public int Y;           // mPosition.Y
        public int Width;       // mImage.Width
        public int Height;      // mImage.Height
        public float Speed;     // mSpeedMultiplier
        
        //Private members
        protected Texture2D mImage;

        protected Vector2 mPosition;
        protected Vector2 mVelocity;
        protected Vector2 mAcceleration;
        protected float mSpeedMultiplier = 1.0f;

        //Constructors
        public Sprite(Texture2D texture, Vector2 pos, Vector2 vel)
        {
            mImage = texture;
            mPosition = pos;
            mVelocity = vel;
            setQAs();
        }
        public Sprite(Texture2D texture, Vector2 pos, Vector2 vel, float spd)
        {
            mImage = texture;
            mPosition = pos;
            mVelocity = vel;
            mSpeedMultiplier = spd;
            setQAs();
        }
        public Sprite(Texture2D texture, Vector2 pos, Vector2 vel, Vector2 acc)
        {
            mImage = texture;
            mPosition = pos;
            mVelocity = vel;
            mAcceleration = acc;
            setQAs();
        }
        public Sprite(Texture2D texture, Vector2 pos, Vector2 vel, Vector2 acc, float spd)
        {
            mImage = texture;
            mPosition = pos;
            mVelocity = vel;
            mAcceleration = acc;
            mSpeedMultiplier = spd;
            setQAs();
        }

        //General methods
        public void CheckBounds(int lower, int upper)
        {
            mPosition.X = SCUtil.Max(0, SCUtil.Min((int)mPosition.X, upper - mImage.Width));
            setQAs();
        }

        public void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(mImage, mPosition, Color.White);
        }
        public void Draw(SpriteBatch sprBatch, float Wscale, float Hscale)
        {
            sprBatch.Draw(mImage, new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)(mImage.Width * Wscale), (int)(mImage.Height * Hscale)), Color.White);
        }
        public void Draw(SpriteBatch sprBatch, float radians, Color color)
        {
            sprBatch.Draw(mImage, new Rectangle((int)mPosition.X, (int)mPosition.Y, mImage.Width, mImage.Height), null, color, radians, new Vector2(0, 0), SpriteEffects.None, 0);
        }
        public void DrawFlipped(SpriteBatch sprBatch, float Wscale, float Hscale)
        {
            sprBatch.Draw(mImage, new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)(mImage.Width * Wscale), (int)(mImage.Height * Hscale)), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }

        public Vector2 getVelocity()
        {
            return mVelocity;
        }

        public Rectangle Hitbox()
        {
            return (new Rectangle((int)mPosition.X, (int)mPosition.Y, mImage.Width, mImage.Height));
        }

        public void HorizontalBounce()
        {
            mVelocity.X *= -1;
            mPosition.X += mVelocity.X * mSpeedMultiplier;
            setQAs();
        }

        public void ReloadImage(Texture2D texture)
        {
            mImage = texture;
            setQAs();
        }

        public void setSpeedMultiplier(float mult)
        {
            mSpeedMultiplier = mult;
            setQAs();
        }

        public void setPosition(float x, float y)
        {
            mPosition.X = x;
            mPosition.Y = y;
            setQAs();
        }

        public void setVelocity(float x, float y)
        {
            mVelocity.X = x;
            mVelocity.Y = y;
        }

        public void Update()
        {
            mPosition.X += mVelocity.X * mSpeedMultiplier;
            mPosition.Y += mVelocity.Y * mSpeedMultiplier;
            setQAs();
        }

      

        //Private methods
        private void setQAs()
        {
            X = (int)mPosition.X;
            Y = (int)mPosition.Y;
            Width = mImage.Width;
            Height = mImage.Height;
            Speed = mSpeedMultiplier;
        }
    }
}
