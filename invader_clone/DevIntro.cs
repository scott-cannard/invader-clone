using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace invader_clone
{
    class DevIntro
    {
        private const int delay = 12;

        private Texture2D[] mFrame;
        private int mFrameCount;
        private long mCycles = 0;

        public DevIntro(Texture2D[] frameSet, int frames)
        {
            mFrame = new Texture2D[frames];
            mFrameCount = frames;
            for (int f = 0;  f < frames;  f++)
                 mFrame[f] = frameSet[f];

        }

        public void Display(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(mFrame[(int)(mCycles++ / delay)], new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
        }

        public gamePhase HandleInput(ButtonEvents bEvent, gamePhase same)
        {
            gamePhase result = same;

            if (bEvent.StartPress || bEvent.EnterPress)
                result = gamePhase.SPLASH;

            return result;
        }

        public bool IsDone()
        {
            if ((int)(mCycles / delay) == mFrameCount)
                return true;
            else
                return false;
        }
    }
}
