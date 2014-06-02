using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace invader_clone
{
    class SplashScreen
    {
        private class SelectOption
        {
            public int option = 0;
            public Color color(int index)
            {
                if (index - 1 == option)
                    return Color.Tomato;
                else
                    return Color.Ivory;
            }
        }
        private SelectOption difficulty = new SelectOption();

        private SpriteFont fontFkey;
        private SpriteFont fontTitle;
        private SpriteFont fontInstruction;
        private bool mFirstPass = true;

        public SplashScreen(SpriteFont fkey, SpriteFont title, SpriteFont instruction)
        {
            fontFkey = fkey;
            fontTitle = title;
            fontInstruction = instruction;
        }

        public void Draw(GraphicsDevice gDev, SpriteBatch sprBatch, Starfield stars, UFOManager ufo)
        {
            stars.Draw(sprBatch);
            sprBatch.DrawString(fontFkey, "F-Key Entertainment presents:", new Vector2(gDev.Viewport.Width * 0.23f, gDev.Viewport.Height * 0.02f), Color.CornflowerBlue);
            sprBatch.DrawString(fontTitle, "Alien", new Vector2(300, -20), new Color(220, 185, 30));
            sprBatch.DrawString(fontTitle, "Incursion", new Vector2(40, 125), new Color(220, 185, 30));
            ufo.SplashDraw(sprBatch, gDev);
            sprBatch.DrawString(fontFkey, "Easy", new Vector2(600, 525), difficulty.color(1));
            sprBatch.DrawString(fontFkey, "Medium", new Vector2(575, 570), difficulty.color(2));
            sprBatch.DrawString(fontFkey, "Hard", new Vector2(600, 615), difficulty.color(3));
            sprBatch.DrawString(fontInstruction, "Select difficulty using D-Pad, Press 'Start' to play", new Vector2(300, 675), Color.LightGray);
        }

        public gamePhase HandleInput(ButtonEvents bEvent, gamePhase same, UFOManager ufo, Player player)
        {
            gamePhase result = same;

            if (bEvent.BackPress || bEvent.EscPress)
            {
                // Allows the game to exit
                result = gamePhase.QUIT;
            }
            else if (bEvent.DpadDownPress || bEvent.DownPress)
            {
                // Move difficulty selector down
                difficulty.option = (difficulty.option + 1) % 3;
            }
            else if (bEvent.DpadUpPress || bEvent.UpPress)
            {
                // Move difficulty selector up
                difficulty.option = (difficulty.option + 2) % 3;
            }
            else if (bEvent.StartPress || bEvent.EnterPress)
            {
                // Begin game
                switch (difficulty.option)
                {
                    case 0: //Easy
                        player.level = 1;
                        player.score = 0;
                        player.lives = 10;
                        player.maxBullets = 8;
                        player.bulletSpeed = 3;
                        player.setSpeedMultiplier(8);
                        break;

                    case 1: //Medium
                        player.level = 1;
                        player.score = 0;
                        player.lives = 5;
                        player.maxBullets = 2;
                        player.bulletSpeed = 1.5f;
                        player.setSpeedMultiplier(5);
                        break;

                    case 2: //Hard
                        player.level = 1;
                        player.score = 0;
                        player.lives = 3;
                        player.maxBullets = 1;
                        player.bulletSpeed = 1;
                        player.setSpeedMultiplier(3);
                        break;
                }
                ufo.Destroy();
                result = gamePhase.LOADLEVEL;
                mFirstPass = true;         //reset splash screen initializer
            }

            return result;
        }

        public void SplashDance(GraphicsDevice gDev, UFOManager ufo)
        {
            if (mFirstPass)
            {
                ufo.SplashCreate(gDev);
                mFirstPass = false;
            }
            ufo.SplashDance(gDev);
        }
    }
}
