using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Immortals
{
    /// <summary>Main game engine.</summary>
    public class ImmortalsEngine : Microsoft.Xna.Framework.Game
    {
        // Graphics managers
        GraphicsDeviceManager graphics;
        GameView gameView;

        // Mouse Variables
        MouseState prevMouseState;

        // Random Number Generator
        public Random rnd { get; protected set; }

        /// <summary>
        /// Constructor for the main engine class.</summary>
        public ImmortalsEngine()
        {
            // Set up the game window
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 1050;

            // set up the Content path
            Content.RootDirectory = "Content";

            // set up the game View
            gameView = new GameView(this);
            Components.Add(gameView);

            // set up randomizer
            this.rnd = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before 
        /// starting to run. This is where it can query for any required 
        /// services and load any non-graphics related content. Calling 
        /// base.Initialize will enumerate through any components and 
        /// initialize them as well.</summary>
        protected override void Initialize()
        {
            // make visible the mouse
            this.IsMouseVisible = true;

            // Initialize base class
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.</summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to 
        /// unload all content.</summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>Updates all high-level game objects.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // input states
            MouseState mouseState;
            KeyboardState keyboardState;

            // Poll devices
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            // save the mouse state for next cyle
            prevMouseState = mouseState;

            // update the base class
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself. This shouldn't be 
        /// drawing anything.</summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear all the old stuff.
            GraphicsDevice.Clear(Color.Black);

            // Draw everything else.
            base.Draw(gameTime);
        }

        /// <summary>Function to get the least of 2 ints.</summary>
        /// <param name="a">The first int to compare</param>
        /// <param name="b">The second int to compare</param>
        /// <returns>The least of the 2 ints</returns>
        public int leastInt(int a, int b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }

        /// <summary>Function to get the greatest of 2 ints.</summary>
        /// <param name="a">The first int to compare</param>
        /// <param name="b">The second int to compare</param>
        /// <returns>The greatest of the 2 ints</returns>
        public int greaterInt(int a, int b)
        {
            if (a >= b)
                return a;
            else
                return b;
        }
    }
}