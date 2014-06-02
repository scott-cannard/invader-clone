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
using Scott.Sprites;
using Scott.Utilities;

namespace invader_clone
{
    // "Global" data types
    enum gamePhase { DEVINTRO, SPLASH, HIGHSCORES, LOADLEVEL, PLAY, PAUSE, GAMEOVER, QUIT };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ButtonEvents bEvents;
        gamePhase phase;

        DevIntro scrIntro;
        SplashScreen scrSplash;
        LevelLoader scrLoader;
        PlayScreen scrPlay;
        PauseScreen scrPause;
        GameOver scrGameover;

        Starfield stars;
        Player player;
        AlienManager aliens;
        UFOManager ufo;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize game window
            base.Initialize();

            // Everything else
            bEvents = new ButtonEvents(GamePad.GetState(PlayerIndex.One), Keyboard.GetState());
            phase = gamePhase.DEVINTRO;

            Texture2D[] frames = new Texture2D[31] { Content.Load<Texture2D>("Intro/devint-01"),
                                                     Content.Load<Texture2D>("Intro/devint-02"),
                                                     Content.Load<Texture2D>("Intro/devint-03"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-04b"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-04b"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-04b"),
                                                     Content.Load<Texture2D>("Intro/devint-04a"),
                                                     Content.Load<Texture2D>("Intro/devint-05"),
                                                     Content.Load<Texture2D>("Intro/devint-06"),
                                                     Content.Load<Texture2D>("Intro/devint-07"),
                                                     Content.Load<Texture2D>("Intro/devint-08"),
                                                     Content.Load<Texture2D>("Intro/devint-09"),
                                                     Content.Load<Texture2D>("Intro/devint-10"),
                                                     Content.Load<Texture2D>("Intro/devint-11"),
                                                     Content.Load<Texture2D>("Intro/devint-12"),
                                                     Content.Load<Texture2D>("Intro/devint-13"),
                                                     Content.Load<Texture2D>("Intro/devint-14"),
                                                     Content.Load<Texture2D>("Intro/devint-15"),
                                                     Content.Load<Texture2D>("Intro/devint-16"),
                                                     Content.Load<Texture2D>("Intro/devint-17"),
                                                     Content.Load<Texture2D>("Intro/devint-18"),
                                                     Content.Load<Texture2D>("Intro/devint-19"),
                                                     Content.Load<Texture2D>("Intro/devint-19"),
                                                     Content.Load<Texture2D>("Intro/devint-19"),
                                                     Content.Load<Texture2D>("Intro/devint-19"),
                                                     Content.Load<Texture2D>("Intro/devint-19") };

            scrIntro = new DevIntro(frames, 31);
            scrSplash = new SplashScreen(Content.Load<SpriteFont>("Fonts/Fkey"),
                                         Content.Load<SpriteFont>("Fonts/Title"),
                                         Content.Load<SpriteFont>("Fonts/Instruction"));
            scrLoader = new LevelLoader(Content.Load<SpriteFont>("Fonts/Level"));
            scrPlay = new PlayScreen(Content.Load<SpriteFont>("Fonts/PlayerHeader"),
                                     Content.Load<SpriteFont>("Fonts/PlayerData"),
                                     Content.Load<Texture2D>("Panel"));
            scrPause = new PauseScreen(Content.Load<SpriteFont>("Fonts/Pause"));
            scrGameover = new GameOver(Content.Load<SpriteFont>("Fonts/GameOver"));

            stars = new Starfield(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("Ship"),
                                Content.Load<Texture2D>("Bullet"),
                                Content.Load<SoundEffect>("Sounds/ShipFiring"),
                                Content.Load<SoundEffect>("Sounds/ShipDeath"),
                                new Vector2((GraphicsDevice.Viewport.Width - 200) / 2 - 40, GraphicsDevice.Viewport.Height - 91),
                                new Vector2(0, 0),
                                4.0f);
            aliens = new AlienManager(Content.Load<Texture2D>("alien0"),
                                      Content.Load<Texture2D>("alien1"),
                                      Content.Load<Texture2D>("alien2"),
                                      Content.Load<Texture2D>("alien3"),
                                      Content.Load<Texture2D>("Bullet"),
                                      Content.Load<SoundEffect>("Sounds/AlienFiring"),
                                      Content.Load<SoundEffect>("Sounds/AlienDeath"));
            ufo = new UFOManager(Content.Load<Texture2D>("BigAlien"),
                                 Content.Load<Texture2D>("Present"),
                                 Content.Load<SoundEffect>("Sounds/UFOMoving"),
                                 Content.Load<SoundEffect>("Sounds/GiftboxDrop"),
                                 Content.Load<SoundEffect>("Sounds/GiftboxCollect"));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            bEvents.Update(GamePad.GetState(PlayerIndex.One), Keyboard.GetState());

            switch (phase)
            {
                case gamePhase.DEVINTRO:
                    phase = scrIntro.HandleInput(bEvents, phase);
                    if (scrIntro.IsDone())
                        phase = gamePhase.SPLASH;
                    break;

                case gamePhase.SPLASH:
                    //Move objects
                    scrSplash.SplashDance(GraphicsDevice, ufo);
                    //Check for input
                    phase = scrSplash.HandleInput(bEvents, phase, ufo, player);
                    break;

                case gamePhase.HIGHSCORES:
                    break;

                case gamePhase.LOADLEVEL:
                    //Initialize for play state
                    scrLoader.Initialize(player, aliens, ufo);
                    //Move objects
                    scrLoader.Update(stars, player, aliens, ufo, GraphicsDevice);
                    //Check timer
                    if (scrLoader.IsDone())
                        phase = gamePhase.PLAY;
                    break;

                case gamePhase.PLAY:
                    //Move non-player objects
                    if (!scrPlay.Update(stars, player, aliens, ufo, GraphicsDevice))
                        phase = gamePhase.GAMEOVER;   //aliens have reached the bottom

                    //Random appearances
                    ufo.Spawn(GraphicsDevice, player.level);
                    aliens.Fire(player.level);

                    //Check for input
                    phase = scrPlay.HandleInput(bEvents, phase, player, GraphicsDevice);

                    //Check for collisions
                    scrPlay.DetectCollisions(player, aliens, ufo);

                    //Change of state?
                    if (player.lives == 0)
                        phase = gamePhase.GAMEOVER;
                    else if (aliens.count() == 0)
                    {
                        player.level++;
                        phase = gamePhase.LOADLEVEL;
                    }
                    break;

                case gamePhase.PAUSE:
                    phase = scrPause.HandleInput(bEvents, phase);
                    break;

                case gamePhase.GAMEOVER:                                      
                    //Move objects
                    stars.Update(GraphicsDevice);
                    //Check for input
                    phase = scrGameover.HandleInput(bEvents, phase);
                    break;

                case gamePhase.QUIT:
                default:
                    this.Exit();
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (phase)
            {
                case gamePhase.DEVINTRO:
                    spriteBatch.Begin();
                    scrIntro.Display(spriteBatch, GraphicsDevice);
                    spriteBatch.End();
                    break;

                case gamePhase.SPLASH:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    scrSplash.Draw(GraphicsDevice, spriteBatch, stars, ufo);
                    spriteBatch.End();
                    break;

                case gamePhase.LOADLEVEL:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    scrLoader.Draw(GraphicsDevice, spriteBatch, stars, player, aliens, ufo);
                    spriteBatch.End();
                    break;

                case gamePhase.PLAY:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    scrPlay.Draw(spriteBatch, stars, player, aliens, ufo, true, true);
                    spriteBatch.End();
                    break;

                case gamePhase.PAUSE:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    scrPause.Draw(spriteBatch, GraphicsDevice, stars, player, aliens, ufo);
                    spriteBatch.End();
                    break;

                case gamePhase.GAMEOVER:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    scrGameover.Draw(spriteBatch, GraphicsDevice, stars, player, aliens, ufo);
                    spriteBatch.End();
                    break;

                case gamePhase.QUIT:
                default:
                    GraphicsDevice.Clear(Color.Black);
                    break;
            }
            
            base.Draw(gameTime);
        }
    }
}
