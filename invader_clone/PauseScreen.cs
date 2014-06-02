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
    class PauseScreen : PlayScreen
    {
        private SpriteFont fontPause;

        public PauseScreen(SpriteFont pause)
            : base()
        {
            fontPause = pause;
        }

        public void Draw(SpriteBatch sprBatch, GraphicsDevice gDev, Starfield stars, Player player, AlienManager aliens, UFOManager ufo)
        {
            base.Draw(sprBatch, stars, player, aliens, ufo, true, true);
            sprBatch.DrawString(fontPause, "PAUSE", new Vector2(gDev.Viewport.Width * 0.21f, gDev.Viewport.Height * 0.25f), new Color(180, 15, 20));
        }

        public gamePhase HandleInput(ButtonEvents bEvent, gamePhase same)
        {
            gamePhase result = same;

            if (bEvent.StartPress || bEvent.EnterPress)
                result = gamePhase.PLAY;

            if (bEvent.BackPress || bEvent.EscPress)
                result = gamePhase.SPLASH;

            return result;
        }
    }
}
