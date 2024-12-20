﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Eventing.Reader;

namespace Summative_animation_project
{
    enum Screen
    {
        Intro,
        play,
        touchDown
    }
    

    

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SoundEffect cheer;
        MouseState mouseState;
        Texture2D footallField;
        Texture2D huddle;
        Rectangle window;
        Texture2D playerRunning;
        Rectangle playerRunningRect;
        Texture2D tacklingPlayer;
        Vector2 playerSpeed;
        Screen screen;
        Texture2D touchDown;
        SpriteFont timeFont;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 600);
            playerSpeed = new Vector2(2, 0);
            playerRunningRect = new Rectangle(84, 320, 150, 150);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            base.Initialize();
            screen = Screen.Intro;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            footallField = Content.Load<Texture2D>("footballfield");
            huddle = Content.Load<Texture2D>("huddle");
            playerRunning = Content.Load <Texture2D>("playerRunning");
            tacklingPlayer = Content.Load<Texture2D>("tacklingPlayer");
            touchDown = Content.Load<Texture2D>("touchDown");
            cheer = Content.Load<SoundEffect>("cheer");
            timeFont = Content.Load<SpriteFont>("TextFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.play;
            }
            else if (screen == Screen.play)
            {
                playerRunningRect.X += (int)playerSpeed.X;
                playerRunningRect.Y += (int)playerSpeed.Y;

                //if (playerRunningRect.Right > window.Width)
                //    playerSpeed.X *= -1;
                if (playerRunningRect.X>850)
                {
                    screen = Screen.touchDown;
                    cheer.Play();

                }

                if (playerRunningRect.X>325)
                {
                    playerSpeed.Y = -2;
                }
                if (playerRunningRect.X > 480)
                {
                    playerSpeed.Y -= -4;
                }
                if (playerRunningRect.Y > 320)
                {
                    playerSpeed.Y = 0;
                }

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(huddle, window, Color.White);
                _spriteBatch.DrawString(timeFont, "Left click anywhere to call the play, coach!", new Vector2(32, 470), Color.Red);

            }
            else if (screen == Screen.play)
            {
                _spriteBatch.Draw(footallField, window, Color.White);
                _spriteBatch.Draw(playerRunning, playerRunningRect, Color.White);
                _spriteBatch.Draw(tacklingPlayer, new Rectangle(450, 330, 150, 150), Color.White);
            }
            else if (screen == Screen.touchDown)
            {
                _spriteBatch.Draw(touchDown, window, Color.White);
                _spriteBatch.DrawString(timeFont, "TOUCHDOWNNNNN!", new Vector2(550, 300), Color.Red);
            }
           
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
