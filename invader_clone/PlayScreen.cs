using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scott.CollisionDetection;
using Scott.Sprites;

namespace invader_clone
{
    class PlayScreen    //***Superclass***
    {
        private static SpriteFont fontHeader;
        private static SpriteFont fontData;
        private static Texture2D mPanel;

        public PlayScreen()
        { }
        public PlayScreen(SpriteFont header, SpriteFont data, Texture2D panel)
        {
            fontHeader = header;
            fontData = data;
            mPanel = panel;
        }

        public void DetectCollisions(Player player, AlienManager aliens, UFOManager ufo)
        {
            aliens.DetectCollision(player.bullets(), player);
            ufo.DetectCollision(player.bullets(), player.level);
            if (player.DetectCollision(ufo.Gift()))
                ufo.CatchGift(player);
            player.DetectCollision(aliens.bullets());
        }

        public void Draw(SpriteBatch sprBatch, Starfield stars, Player player, AlienManager aliens, UFOManager ufo, bool drawNPCs, bool drawShip)
        {
            stars.Draw(sprBatch);
            if (drawShip)
            {
                player.Draw(sprBatch);
                foreach (Sprite pB in player.bullets())
                    pB.Draw(sprBatch);
            }
            if (drawNPCs)
            {
                foreach (Sprite eB in aliens.bullets())
                    eB.Draw(sprBatch, (float)Math.PI, Color.Red);
                foreach (Sprite a in aliens.alienList())
                    a.Draw(sprBatch);
                if (!ufo.Caught())
                    ufo.Draw(sprBatch);
            }
            sprBatch.Draw(mPanel, new Vector2(1080, 0), Color.White);
            sprBatch.DrawString(fontHeader, ("Level   " + player.level), new Vector2(1120, 19), Color.IndianRed);
            sprBatch.DrawString(fontHeader, ("Score"), new Vector2(1135, 95), Color.CadetBlue);
            sprBatch.DrawString(fontData, ("" + player.score), new Vector2(1135, 140), Color.White);
            sprBatch.DrawString(fontHeader, ("Lives:"), new Vector2(1120, 235), Color.CadetBlue);
            sprBatch.DrawString(fontData, ("" + player.lives), new Vector2(1220, 235), Color.White);
            sprBatch.DrawString(fontHeader, ("Ship Speed"), new Vector2(1100, 325), Color.ForestGreen);
            sprBatch.DrawString(fontData, ("" + player.Speed), new Vector2(1162, 374), Color.White);
            sprBatch.DrawString(fontHeader, ("Max Bullets"), new Vector2(1099, 460), Color.ForestGreen);
            sprBatch.DrawString(fontData, ("" + player.maxBullets + " / 8"), new Vector2(1150, 509), Color.White);
            sprBatch.DrawString(fontHeader, ("Bullet Speed"), new Vector2(1095, 600), Color.ForestGreen);
            sprBatch.DrawString(fontData, ("" + player.bulletSpeed), new Vector2(1166, 649), Color.White);
            if (drawNPCs && ufo.Caught())
                ufo.Draw(sprBatch);
        }

        public gamePhase HandleInput(ButtonEvents bEvent, gamePhase same, Player player, GraphicsDevice gDev)
        {
            gamePhase result = same;

            if (bEvent.BackPress || bEvent.EscPress)
                result = gamePhase.SPLASH;

            if (bEvent.StartPress || bEvent.EnterPress)
                result = gamePhase.PAUSE;

            if ((bEvent.APress || bEvent.SpacePress) && player.bullets().Count < player.maxBullets)
                player.Fire();

            player.setPosition(player.X + (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player.Speed), player.Y);
            //*****
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                player.setPosition((player.X - player.Speed), player.Y);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.setPosition((player.X + player.Speed), player.Y);
            //*****
            player.CheckBounds(0, gDev.Viewport.Width - 200);

            return result;
        }

        public bool Update(Starfield stars, Player player, AlienManager aliens, UFOManager ufo, GraphicsDevice gDev)
        {
            bool gameover = false;

            stars.Update(gDev);
            gameover = !aliens.Update(gDev, player.Y);
            ufo.Update(gDev, player);
            player.UpdateBullets();

            return (!gameover);
        }
    }
}
