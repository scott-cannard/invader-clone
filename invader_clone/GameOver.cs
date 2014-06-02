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
    class GameOver : PlayScreen
    {
        private SpriteFont fontGameOver;

        public GameOver(SpriteFont gameover)
            : base()
        {
            fontGameOver = gameover;
        }

        public void Draw(SpriteBatch sprBatch, GraphicsDevice gDev, Starfield stars, Player player, AlienManager aliens, UFOManager ufo)
        {
            base.Draw(sprBatch, stars, player, aliens, ufo, true, false);
            sprBatch.DrawString(fontGameOver, "GAME OVER", new Vector2(gDev.Viewport.Width * 0.07f, gDev.Viewport.Height * 0.3f), new Color(180, 15, 20));
        }

        public gamePhase HandleInput(ButtonEvents bEvent, gamePhase same)
        {
            gamePhase result = same;

            if (bEvent.BackPress || bEvent.EscPress)
                result = gamePhase.SPLASH;

            return result;
        }
    }
}
