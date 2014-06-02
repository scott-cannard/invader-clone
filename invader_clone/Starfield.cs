using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace invader_clone
{
    class Starfield
    {
        private static Texture2D starTexture;
        private static Random column = new Random();
        private static int numberOfStars = 250;
        private Vector2[] star;

        
        public Starfield(GraphicsDevice gDev)
        {
            starTexture = new Texture2D(gDev, 1, 1, false, SurfaceFormat.Color);
            starTexture.SetData<Int32>(new Int32[]{0xFFFFFF});

            star = new Vector2[numberOfStars];
            for (int i = 0; i < numberOfStars; i++)
            {
                star[i].X = column.Next(0, gDev.Viewport.Width);    //select random column
                star[i].Y = column.Next(0, gDev.Viewport.Height);   //select random row
            }
        }

        public void Update(GraphicsDevice gDev)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                star[i].Y += 1 + (i % 3);
                if (star[i].Y > gDev.Viewport.Height)
                {
                    star[i].X = column.Next(0, gDev.Viewport.Width);
                    star[i].Y = 0;
                }
            }
        }

        public void Draw(SpriteBatch sprBatch)
        {
            for (int i = 0; i < numberOfStars; i++)
                sprBatch.Draw(starTexture, new Rectangle((int)star[i].X, (int)star[i].Y, 1, 1), Color.White);
        }
    }
}
