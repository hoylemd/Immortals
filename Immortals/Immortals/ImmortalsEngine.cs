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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ImmortalsEngine : Microsoft.Xna.Framework.Game
    {
        // graphics managers
        GraphicsDeviceManager graphics;
        GameView gameView;

        // Texture variables
        Texture2D pixel;

        // Mouse Variables
        MouseState prevMouseState;

        // Game state variables
        // Boolean selectingState;
        Rectangle selectionRect;
        Vector2 selectionOrigin;

        // Sprite Pointers
        Sprite thing;

        // Random Number Generator
        public Random rnd { get; protected set; }

        /// <summary>
        /// Function to get the least of 2 ints
        /// </summary>
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

        /// <summary>
        /// Function to get the greatest of 2 ints
        /// </summary>
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

        /// <summary>
        /// Constructor for the main engine class
        /// </summary>
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
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // make visible the mouse
            this.IsMouseVisible = true;

            // initialize game state variables
            // selectingState = false;

            // Create the "pixel" object
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            // Initialize base class
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

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
            // input states
            MouseState mouseState;
            KeyboardState keyboardState;
            
            // middleman variables
            Point scrollDirection = new Point(0,0);

            // Poll devices
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            // Mouse input
            // Mouse left button down.
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //Console.Out.WriteLine("down.");
                if (prevMouseState.LeftButton == ButtonState.Released)
                {
                    // selectingState = true;
                    selectionOrigin.X = mouseState.X;
                    selectionOrigin.Y = mouseState.Y;
                    selectionRect = new Rectangle(mouseState.X, mouseState.Y, 0, 0);
                }
                else
                {
                   // generate the selection rect
                    selectionRect.X = leastInt((int)selectionOrigin.X, (int)mouseState.X);
                    selectionRect.Y = leastInt((int)selectionOrigin.Y, (int)mouseState.Y);
                    selectionRect.Width = greaterInt((int)selectionOrigin.X, (int)mouseState.X) -
                        leastInt((int)selectionOrigin.X, (int)mouseState.X);
                    selectionRect.Height = greaterInt((int)selectionOrigin.Y, (int)mouseState.Y) -
                        leastInt((int)selectionOrigin.Y, (int)mouseState.Y);
                }
            }
            // Mouse left button up.
            if (mouseState.LeftButton == ButtonState.Released 
                && prevMouseState.LeftButton == ButtonState.Pressed)
            {
                //selectingState = false;
            }
            // Mouse zooming
            if (mouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue && false)
            {
                Console.Out.WriteLine("Scroll wheel moved: " + mouseState.ScrollWheelValue);
                if ((mouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue) > 0)
                    gameView.Zoom(true);
                else
                    gameView.Zoom(false);
    
            }

            // Keyboard Input
            if (keyboardState.IsKeyDown(Keys.Up))
                scrollDirection.Y -=1;
            if (keyboardState.IsKeyDown(Keys.Down))
                scrollDirection.Y += 1;
            if (keyboardState.IsKeyDown(Keys.Right))
                scrollDirection.X += 1;
            if (keyboardState.IsKeyDown(Keys.Left))
                scrollDirection.X -= 1;

            if (scrollDirection != Point.Zero)
                gameView.Pan(scrollDirection);

            // Update the view
            this.gameView.Update();

            // save the mouse state for next cyle
            prevMouseState = mouseState;

            // update the base class
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself. This shouldn't be drawing anything.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}