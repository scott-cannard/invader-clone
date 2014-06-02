using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Scott.Sprites;

namespace invader_clone
{
    class LevelLoader : PlayScreen
    {
        private SpriteFont fontLevel;
        private long mTimer = 0;

        public LevelLoader(SpriteFont level)
            : base()
        {
            fontLevel = level;
        }

        public void Draw(GraphicsDevice gDev, SpriteBatch spriteBatch, Starfield stars, Player player, AlienManager aliens, UFOManager ufo)
        {
            base.Draw(spriteBatch, stars, player, aliens, ufo, false, false);
            if (player.level < 10)
                spriteBatch.DrawString(fontLevel, ("Level " + player.level), new Vector2(gDev.Viewport.Width * 0.15f, gDev.Viewport.Height * 0.25f), Color.DarkGreen);
            else
                spriteBatch.DrawString(fontLevel, ("Level " + player.level), new Vector2(gDev.Viewport.Width * 0.11f, gDev.Viewport.Height * 0.25f), Color.DarkGreen);
        }

        public void Initialize(Player player, AlienManager aliens, UFOManager ufo)
        {
            if (mTimer == 0)
            {
                player.bullets().Clear();
                aliens.Clear();
                ufo.Destroy(); 
                aliens.Spawn(player.level);
            }
        }

        public bool IsDone()
        {
            if (mTimer > 150)
            {
                mTimer = 0;
                return true;
            }
            else
                return false;
        }

        public new bool Update(Starfield stars, Player player, AlienManager aliens, UFOManager ufo, GraphicsDevice gDev)
        {
            mTimer++;
            return (base.Update(stars, player, aliens, ufo, gDev));
        }
    }
}
